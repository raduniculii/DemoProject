// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}
function CreateRouter(strBaseUrl, oRoutes){
    strBaseUrl = (new URL(strBaseUrl, document.location.origin)).href;
    function navToNewUrl(strNewUrl, strNewTitle, blReplaceIfMatching){
        var currentUrl = new URL(document.location.href);
        var newUrl = new URL(strBaseUrl + strNewUrl, document.location.href);
        if(newUrl.href == currentUrl.href) return;
    
        //currentUrl.searchParams.set("max", pSize);
        //`${document.location.origin}${document.location.pathname}${document.location.search}${document.location.hash}`
        if(`${currentUrl.origin}${currentUrl.pathname}` == `${newUrl.origin}${newUrl.pathname}` && blReplaceIfMatching){
            history.replaceState({}, strNewTitle, newUrl.href);
        }
        else {
            history.pushState({}, strNewTitle, newUrl.href);
        }
    
        window.dispatchEvent(new Event('popstate'));
    }

    function parseRoute(r, fcHandler){
        var routeParams = [];
        var queryParams = [];
        var paramName = [];
        var collecting = false;
        var inQuery = false;
        var ix = 0;

        while(ix < r.length){
            var chr = r[ix];
            if(collecting){
                if(chr == "}"){
                    if(inQuery) queryParams.push(paramName.join(""));
                    else routeParams.push(paramName.join(""));

                    paramName.splice(0);
                    collecting = false;
                }
                else paramName.push(chr);
            }
            else if(chr == "{"){
                collecting = true;
            }
            else if(chr == "?"){
                inQuery = true;
            }
            ix++;
        }

        var strRoute = (inQuery ? r.substring(0, r.indexOf("?")) : r);
        var rxRouteMatcher = new RegExp("^" + escapeRegExp(strRoute.replace(/^[\\\/]+/, "")).replace(/\\\{[^}]*\\\}/g, "([^\\\\/?]*)") + "$");

        return {
            activate: function(strNewTitle, objData){
                var route = strRoute;
                routeParams.forEach(paramName => {
                    if(objData[paramName] || objData[paramName] === 0){
                        route = route.replace(new RegExp("\\{" + paramName + "\\}"), objData[paramName]);
                    }
                    else route = route.replace(new RegExp("[\\\\\\/]?\\{" + paramName + "\\}"), "");
                });
    
                var qs = new URLSearchParams();
                queryParams.forEach(paramName => {
                    if(objData[paramName] || objData[paramName] === 0){
                        qs.set(paramName, objData[paramName]);
                    }
                });

                var strQs = qs.toString();
                navToNewUrl((!route && !!strQs ? "?" : "") + [route, strQs].filter(s => !!s).join("?"), strNewTitle, true);
            }
            , matchCurrent(){
                var relPath = `${document.location.origin}${document.location.pathname}`;
                if(!relPath.startsWith(strBaseUrl)) throw new Error(`The new location doesn't start with the expected base URL: '${strBaseUrl}'`);
                
                relPath = relPath.substring(strBaseUrl.length);
                
                relPath = relPath.replace(/^[\\\/]+/g, "");
                
                var matches = rxRouteMatcher.exec(relPath);
                if(matches){
                    var objData = {};
                    routeParams.forEach((p, ix) => {
                        objData[p] = matches[ix + 1];
                    });
                    var qs = new URLSearchParams(document.location.href);
                    queryParams.forEach(p => {
                        if(qs.has(p)){
                            objData[p] = qs.get(p);
                        }
                    });
                    fcHandler(relPath, objData);
                    return true;
                }
                else return false;
            }
        };
    }

    //oRoutes = { "route/{id}?{name1}{name2}": fcHandler(oData){} };
    var parsedRoutes = {};
    for(var r in oRoutes){
        parsedRoutes[r] = parseRoute(r, oRoutes[r]);
    }

    function parseCurrentUrl(evt){
        for(var r in parsedRoutes){
            if(parsedRoutes[r].matchCurrent()) break;
        }
    }
    
    return {
        activate: function(routeSpec, strNewTitle, objData){
            parsedRoutes[routeSpec].activate(strNewTitle, objData);
        }
        , doStartup: function(){
            window.addEventListener("popstate", parseCurrentUrl);

            if(document.readyState === "complete" || document.readyState === "interactive"){
                parseCurrentUrl();
            }
            else {
                window.addEventListener("load", parseCurrentUrl);
            }
        }
    }
}
function CreateSortToggler(sortedBy = [], setSort = (col, isMain) => {} ){
    return function (col, startOver){
        col.dir = (col.dir == "asc" ? "desc" : "asc");
        
        if(startOver){
            sortedBy.splice(0).forEach((sCol) => {
                if(sCol != col) {
                    sCol.dir = null;
                    setSort(sCol, false);
                }
            });

            setSort(col, true);
            sortedBy.push(col);
        }
        else {
            var itemIndex = sortedBy.findIndex((sCol) => sCol == col);
            
            if(itemIndex < 0) {
                sortedBy.push(col);
            }
            else {
                if(itemIndex != sortedBy.length - 1) {
                    sortedBy.splice(itemIndex, 1);
                    if(itemIndex == 0) {
                        setSort(sortedBy[0], true);
                    }
                    col.dir = "asc";
                    sortedBy.push(col);
                }
            }

            setSort(col, sortedBy[0] == col);
        }
    }
}

function CreateModules(strListTemplateName, strBaseTitle){
    var lt = document.getElementById(strListTemplateName);
    blNotLoaded = true;

    function setLoading(blIsLoading){
        if(blIsLoading){
            if(document.readyState === "complete" || document.readyState === "interactive"){
                document.body.classList.add("loading");
            }
        }
        else {
            blNotLoaded = false;
            document.body.classList.remove("not-loaded");
            document.body.classList.remove("loading");
        }
    }

    document.querySelectorAll("[data-module]").forEach(module => {
        var apiUrl = module.getAttribute("data-module");
        module.removeAttribute("data-module");
        var tbl = module.querySelectorAll("[data-list]")[0];
        tbl.removeAttribute("data-list");
        
        var btnSaveClick = function(){ console.log("old save click"); };
        var divDetails = $(module.querySelectorAll("[data-details]")[0]).modal({ backdrop: "static", keyboard: true, focus: true, show: false });
        divDetails[0].removeAttribute("data-details");
        var _isDirty = false;
        divDetails.on("shown.bs.modal", function(e){
            $(divDetails.find("input, select, textarea")[0]).focus();
        });
        divDetails.find("input, select, textarea").on("change, input", function(evt){ _isDirty = true; });
        divDetails.find("input, select").on("keydown", function(evt){
            if(evt.keyCode == 13) {
                btnSaveClick();
                evt.stopPropagation();
                evt.preventDefault();
            }
        });
        divDetails.on('hide.bs.modal', function (e) {
            // do something...
            if(_isDirty && !confirm("There are some changes that were not saved.\n\nAre you sure you want to cancel them?")) {
                e.preventDefault();
                e.stopPropagation();
            }
            else {
                _isDirty = false;
                mainList.activate();
            }
        });
        var urlQueryString = new URLSearchParams(document.location.search);

        var selectedId = 0;

        var pageUrl = module.getAttribute("data-base-url"); module.removeAttribute("data-base-url");
        var listProgName = module.getAttribute("data-list-prog-name"); module.removeAttribute("data-list-prog-name");

        var router = CreateRouter(pageUrl, {
            "?{pag}{max}{ord}{search}": function(relPath, objData){ mainList.show(objData); histList.hide(); deletedItemsList.hide(); divDetails.modal("hide"); }
            , "/Deleted?{pag}{max}{ord}{search}": function(relPath, objData){ deletedItemsList.show(objData); mainList.hide(); histList.hide(); divDetails.modal("hide"); }
            , "/History?{pag}{max}{ord}{search}": function(relPath, objData){ histList.show(objData); mainList.hide(); deletedItemsList.hide(); divDetails.modal("hide"); }
            , "/Add": function(relPath, objData){ editItem(null); }
            , "/{id}": function(relPath, objData){ editItem(objData.id); }
            , "/{id}/History?{pag}{max}{ord}{search}": function(relPath, objData){ histList.show(objData); mainList.hide(); deletedItemsList.hide(); divDetails.modal("hide"); }
            , "/{id}/History/{ver}": function(relPath, objData){ alert("item hist view version URL: " + JSON.stringify(objData)); }
        });

        var mainList = null;
        var histList = null;
        var deletedItemsList = null;

        var userListSettings = { main: {}, hist: {}, deletedItems: {} };

        setLoading(true);
        $.ajax({
            url: listSettingsApiUrl
            , type: "GET"
            , data: { listProgName: listProgName }
        }).then(function(data){
            var dataMain = data.find(l => l.listProgName = listProgName);
            var dataHist = data.find(l => l.listProgName = listProgName + "_Hist");
            var dataDeleted = data.find(l => l.listProgName = listProgName + "_Deleted");

            userListSettings.main.pageNumber = dataMain.pageNumber;
            userListSettings.main.pageSize = dataMain.pageSize;
            userListSettings.main.sortBy = dataMain.sortBy;
            userListSettings.main.quickSearch = dataMain.quickSearch;
            userListSettings.main.search = dataMain.search;

            userListSettings.hist.pageNumber = dataHist.pageNumber;
            userListSettings.hist.pageSize = dataHist.pageSize;
            userListSettings.hist.sortBy = dataHist.sortBy;
            userListSettings.hist.quickSearch = dataHist.quickSearch;
            userListSettings.hist.search = dataHist.search;

            userListSettings.deletedItems.pageNumber = dataDeleted.pageNumber;
            userListSettings.deletedItems.pageSize = dataDeleted.pageSize;
            userListSettings.deletedItems.sortBy = dataDeleted.sortBy;
            userListSettings.deletedItems.quickSearch = dataDeleted.quickSearch;
            userListSettings.deletedItems.search = dataDeleted.search;

            mainList = createList("main");
            histList = createList("hist");
            deletedItemsList = createList("deletedItems");
    
            router.doStartup();
    
            tbl.parentNode.insertBefore(mainList.element, tbl);
            tbl.parentNode.insertBefore(histList.element, tbl);
            tbl.parentNode.insertBefore(deletedItemsList.element, tbl);
            tbl.parentNode.removeChild(tbl);
    
            setLoading(false);
        }, function(jqXhr, textStatus, errorThrown){
            alert("fail... :-(");
            setLoading(false);
        });

        function setScreenTitle(strNewTitle){
            document.getElementById("hPageTitle").innerText = strNewTitle;
            document.title = strNewTitle;
        }


        function editItem(itemId){
            if(!itemId) {
                btnSaveClick = function(){
                    if(!$(editForm).valid()) return;
                    var objData = {};
                    divDetails.find("input, select, textarea").each((ix, input) => {
                        if(input.type && input.type.toLowerCase() === "hidden") return;
                        
                        var n = input.getAttribute("name");
                        n = n[0].toLowerCase() + n.substring(1);
                        var v = getVal(input);
                        if(v !== null) {
                            objData[n] = v;
                        }
                    });
                    console.log("add: ", objData);
                    setLoading(true);
                    $.ajax({
                        url: apiUrl
                        , type: "POST"
                        , headers: {
                            'Accept': 'application/json'
                            , 'Content-Type': 'application/json'
                        }
                        , data: JSON.stringify(objData)
                        , dataType: "json"
                    }).then(function(data){
                        _isDirty = false;
                        mainList.activate();
                        setLoading(false);
                    }, function(jqXhr, textStatus, errorThrown){
                        if(jqXhr.responseJSON && jqXhr.responseJSON.errors){
                            for(var fld in jqXhr.responseJSON.errors){
                                addFieldError(fld[0].toUpperCase() + fld.slice(1), jqXhr.responseJSON.errors[fld][0]);
                            }
                            setLoading(false);
                            return;
                        }
                        
                        alert("fail... :-(");
                        console.log(jqXhr, textStatus, errorThrown);
                        setLoading(false);
                    });
                }

                showEdit(null);
            }
            else {
                setLoading(true);
                $.ajax({
                    url: apiUrl + "/" + itemId
                    , type: "GET"
                }).then(function(data){
                    btnSaveClick = function(){
                        if(!$(editForm).valid()) return;
                        console.log("new save edit");
                        var objData = { id: itemId, ver: data.ver };
                        divDetails.find("input, select, textarea").each((ix, input) => {
                            var n = input.getAttribute("name");
                            n = n[0].toLowerCase() + n.substring(1);
                            objData[n] = getVal(input);
                        });
                        console.log("edit: ", objData);
                        setLoading(true);
                        $.ajax({
                            url: apiUrl + "/" + itemId
                            , type: "PUT"
                            , headers: {
                                'Accept': 'application/json'
                                , 'Content-Type': 'application/json'
                            }
                            , data: JSON.stringify(objData)
                            , dataType: "json"
                        }).then(function(data){
                            _isDirty = false;
                            mainList.activate();
                            setLoading(false);
                        }, function(jqXhr, textStatus, errorThrown){
                            alert("fail... :-(");
                            setLoading(false);
                        });
                    }

                    showEdit(data);
                    setLoading(false);
                }, function(jqXhr, textStatus, errorThrown){
                    alert("fail... :-(");
                    setLoading(false);
                });
            }

            function resetValidation() {
                //Removes validation from input-fields
                $('.input-validation-error')
                    .addClass('valid')
                    .removeClass('input-validation-error')
                    .attr("aria-invalid", "false")
                    ;
                //Removes validation message after input-fields
                $('.field-validation-error')
                    .removeClass('field-validation-error')
                    .html("")
                    ;
            }

            function addFieldError(name, error){
                $("[name='" + name + "']")
                    .removeClass('valid')
                    .addClass('input-validation-error')
                    .attr("aria-invalid", "true")
                    .focus()
                    ;

                $("[data-valmsg-for='" + name + "']")
                    .addClass('field-validation-error')
                    .append($("<span id='" + name + "-error'>").text(error))
                    ;
            }

            function showEdit(item){
                _isDirty = false;

                resetValidation();

                if(item){
                    divDetails.find("h5.modal-title").text("Edit");
                }
                else {
                    divDetails.find("h5.modal-title").text("Add");
                }
                divDetails.find("input, select, textarea").each((ix, input) => {
                    if(input.type && input.type.toLowerCase() == "hidden") return;
                    if(input.hasAttribute("name")){
                        var n = input.getAttribute("name");
                        n = n[0].toLowerCase() + n.substring(1);
                        if(item != null && n in item){
                            setVal(input, item[n]);
                        }
                        else setVal(input, null);
                    }
                });
    
                if(!_mainListShownOnce) {
                    mainList.show({});
                }
                divDetails.modal("show");
            }
        }
        var btnSave = module.querySelectorAll(`[data-id="btnSave"]`)[0];
        btnSave.removeAttribute("data-id");
        btnSave.addEventListener("click", function(evt){ btnSaveClick(); });

        var editForm = module.querySelectorAll(`[data-id="editForm"]`)[0];
        editForm.removeAttribute("data-id");
        var _mainListShownOnce = false;
        
        function createList(strListType){
            var cl = lt.cloneNode(true);
            cl.style.display = "none";

            function getByDataId(strDataId){
                var e = cl.querySelectorAll(`[data-id="${strDataId}"]`)[0];
                e.removeAttribute("data-id");
                return e;
            }    
            
            var htmlControls = {
                txtQuickSearch: getByDataId("txtQuickSearch")
                , btnRemoveQuickSearch: getByDataId("btnRemoveQuickSearch")
                , btnQuickSearch: getByDataId("btnQuickSearch")
                , btnRemoveSearch: getByDataId("btnRemoveSearch")
                , lnkRowDelete: getByDataId("lnkRowDelete")
                , lnkRowViewItemHist: getByDataId("lnkRowViewItemHist")
                , lnkRowEditItem: getByDataId("lnkRowEditItem")
                , iconInsert: getByDataId("iconInsert")
                , iconUpdate: getByDataId("iconUpdate")
                , iconDelete: getByDataId("iconDelete")
                , headerRow: getByDataId("headerRow")
                , rowButtons: getByDataId("rowButtons")
                , who: getByDataId("who")
                , what: getByDataId("what")
                , when: getByDataId("when")
                , rowCols: getByDataId("rowCols")
                , pagerTd: getByDataId("pagerTd")
                , ddlPageNumber: getByDataId("ddlPageNumber")
                , btnFirst: getByDataId("btnFirst")
                , btnPrev: getByDataId("btnPrev")
                , lblFrom: getByDataId("lblFrom")
                , lblTo: getByDataId("lblTo")
                , lblOutOf: getByDataId("lblOutOf")
                , btnNext: getByDataId("btnNext")
                , btnLast: getByDataId("btnLast")
                , ddlPageSize: getByDataId("ddlPageSize")
                , lnkBack: getByDataId("lnkBack")
                , lnkAdd: getByDataId("lnkAdd")
                , lnkDeletedItems: getByDataId("lnkDeletedItems")
            }

            switch(strListType){
                case "main":
                    htmlControls.lnkBack.classList.add("d-none");
                    
                    htmlControls.lnkDeletedItems.addEventListener("click", function(evt){
                        deletedItemsList.activate();
                        evt.returnValue = false;
                    });
                    break;
                case "hist":
                    htmlControls.lnkAdd.classList.add("d-none");
                    htmlControls.lnkDeletedItems.classList.add("d-none");
                    break;
                case "deletedItems":
                    htmlControls.lnkAdd.classList.add("d-none");
                    htmlControls.lnkDeletedItems.classList.add("d-none");
                    break;
                default:
                    throw "unknown list type";
            }

            htmlControls.lnkBack.addEventListener("click", function(evt){
                window.history.back();
                evt.returnValue = false;
            });

            var cols = [];
            var sortedBy = [];
            var colCreators = [];
            htmlControls.rowButtons.style.width = { "main": "90px", "hist": "", "deletedItems": "30px" }[strListType];
            var buttonsTdClass = htmlControls.rowButtons.getAttribute("data-cell-class");
            htmlControls.rowButtons.removeAttribute("data-cell-class");
            if(strListType != "hist"){
                colCreators.push(function(item){
                    var td = document.createElement("td");
                    td.className = buttonsTdClass;
                    
                    if(strListType == "main"){
                        var lnkDelete = htmlControls.lnkRowDelete.cloneNode(true);
                        td.appendChild(lnkDelete);
                        lnkDelete.addEventListener("click", function(evt){ deleteItem(item); evt.returnValue = false; });
                    }
                    
                    if(strListType == "main" || strListType == "deletedItems"){
                        var lnkViewItemHist = htmlControls.lnkRowViewItemHist.cloneNode(true);
                        td.appendChild(lnkViewItemHist);
                        lnkViewItemHist.addEventListener("click", function(evt){ viewItemHistFor(item); evt.returnValue = false; });
                    }
                    
                    if(strListType == "main"){
                        var lnkEditItem = htmlControls.lnkRowEditItem.cloneNode(true);
                        td.appendChild(lnkEditItem);
                        lnkEditItem.addEventListener("click", function(evt){ router.activate("/{id}", "Edit", { id: item.id }); evt.returnValue = false; });
                    }
        
                    return td;
                });

                if(strListType == "main"){
                    htmlControls.lnkAdd.addEventListener("click", function(evt){ router.activate("/Add", "Add", {}); evt.returnValue = false; });
                }
            }
            else {
                htmlControls.rowButtons.parentNode.removeChild(htmlControls.rowButtons);
            }
    
            for(var i = 0; i < tbl.tBodies[0].rows[0].cells.length; i++){
                var th = tbl.tBodies[0].rows[0].cells[i].cloneNode(true);
                var dataName = th.getAttribute("data-name");
                th.removeAttribute("data-name");
                th.classList.add("sort-link");
                th.title = "Click to change sort";
                var col = { sortBy: dataName, name: dataName, label: th.innerText, dir: null, th: th };
                if(th.hasAttribute("data-sort-by")){
                    col.sortBy = th.getAttribute("data-sort-by");
                    th.removeAttribute("data-sort-by");
                }
                if(th.hasAttribute("data-search-by")){
                    col.searchBy = th.getAttribute("data-search-by").split(",");
                    th.removeAttribute("data-search-by");
                }
                cols.push(col);
                htmlControls.headerRow.insertBefore(th, htmlControls.rowCols);
                colCreators.push((function(dataName){
                    return function(item){
                        var td = document.createElement("td");
                        td.innerText = item[dataName.replace(/^(.)/, (s) => s.toLowerCase())];
                        return td;
                    }
                })(dataName));
    
                th.addEventListener("click", (function(col, toggleSortBy){
                        return function(e){ toggleSortBy(col, !e.ctrlKey); if(sortedBy[0] == col){ pageNumber = 1; } activateMe(); }
                    })(col, CreateSortToggler(sortedBy, setSortGui))
                );
            }
            var histCols = [htmlControls.who, htmlControls.what, htmlControls.when];
            if(strListType == "main"){
                histCols.forEach(hCol => {
                    hCol.parentNode.removeChild(hCol);
                });
            }
            else {
                histCols.forEach(hCol => {
                    if(hCol == htmlControls.what) {
                        if(strListType == "deletedItems"){
                            hCol.parentNode.removeChild(hCol);
                        }
                        return;
                    }
                    var dataName = hCol.getAttribute("data-name")
                    var col = { sortBy: dataName, name: dataName, label: hCol.innerText, dir: null, th: hCol };
                    if(hCol.hasAttribute("data-sort-by")){
                        col.sortBy = hCol.getAttribute("data-sort-by");
                        hCol.removeAttribute("data-sort-by");
                    }
                    cols.push(col);
    
                    hCol.classList.add("sort-link");
                    hCol.addEventListener("click", (function(col, toggleSortBy){
                            return function(e){ toggleSortBy(col, !e.ctrlKey); if(sortedBy[0] == col){ pageNumber = 1; } activateMe(); }
                        })(col, CreateSortToggler(sortedBy, setSortGui))
                    );

                });

                colCreators.push(function(item){
                    var td = document.createElement("td");
                    td.innerText = (item.histActionPerformedBy || "[ unknown user ]");
                    return td;
                });
                if(strListType != "deletedItems"){
                    colCreators.push(function(item){
                        var td = document.createElement("td");
                        td.style.textAlign = "center";
                        td.appendChild({
                                "i": htmlControls.iconInsert.cloneNode(true)
                                , "u": htmlControls.iconUpdate.cloneNode(true)
                                , "d": htmlControls.iconDelete.cloneNode(true)
                            }[item.histActionType]
                        );
                        return td;
                    });
                }
                colCreators.push(function(item){
                    var td = document.createElement("td");
                    td.innerText = item.histActionDateString;
                    return td;
                });
            }
            htmlControls.rowCols.parentNode.removeChild(htmlControls.rowCols);
            
            var listSettings = userListSettings[strListType];
            (listSettings.sortBy || "").split(",").forEach((sortElem, ix) => {
                var dir = "asc";
    
                if(sortElem.startsWith("-")) {
                    sortElem = sortElem.substring(1);
                    dir = "desc";
                }
    
                var col = cols.find(c => c.name == sortElem);
                if(!col || sortedBy.find(c => c == col)) return;
    
                col.dir = dir;
                sortedBy.push(col);
                setSortGui(col, ix == 0);
            });

            htmlControls.pagerTd.colSpan = colCreators.length;
            (urlQueryString.get("ord") || "").split(",").forEach((sortElem, ix) => {
                var label = sortElem;
                var dir = "asc";
    
                if(label.startsWith("-")) {
                    label = label.substring(1);
                    dir = "desc";
                }
    
                var col = cols.find(c => c.label == label);
                if(!col || sortedBy.find(c => c == col)) return;
    
                col.dir = dir;
                sortedBy.push(col);
                setSortGui(col, ix == 0 && sortedBy.length == 1);
            });
    
            function setSortGui(col, isMain){
                var classes = {
                    "sorted-asc": col.dir == "asc"
                    , "sorted-desc": col.dir == "desc"
                    , "sort-main": col.dir && isMain
                }
    
                for(var cls in classes){
                    if(classes[cls]) col.th.classList.add(cls);
                    else col.th.classList.remove(cls);
                }
            }
    
            var pageNumber = listSettings.pageNumber;
            var pageSize = listSettings.pageSize; var pageSizes = [10, 25, 50, 100, 250];
            var totalCount = 0;
            var totalPages = 0;
            function ddlPageNumberChanged(){
                pageNumber = (+htmlControls.ddlPageNumber.options[htmlControls.ddlPageNumber.selectedIndex].value);
                activateMe();
            }
            function ddlPageSizeChanged(){
                pageSize = (+htmlControls.ddlPageSize.options[htmlControls.ddlPageSize.selectedIndex].value);
                pageNumber = 1;
                activateMe();
            }
            function correctDdlPageNumber(){
                htmlControls.ddlPageNumber.removeEventListener("change", ddlPageNumberChanged);
    
                var pageCount = Math.max(totalPages, 1);
                for(var i = htmlControls.ddlPageNumber.options.length; i < pageCount; i++){
                    htmlControls.ddlPageNumber.add(new Option(i + 1, i + 1));
                }
                for(var i = htmlControls.ddlPageNumber.options.length - 1; i >= pageCount; i--){
                    htmlControls.ddlPageNumber.remove(i);
                }
                htmlControls.ddlPageNumber.options[pageNumber - 1].selected = true;
    
                htmlControls.ddlPageNumber.addEventListener("change", ddlPageNumberChanged);
            }
            function correctDdlPageSize(){
                htmlControls.ddlPageSize.removeEventListener("change", ddlPageSizeChanged);
    
                var allPageSizes = pageSizes.concat(pageSizes.findIndex(s => s == pageSize) >= 0 ? [] : [pageSize]);
                allPageSizes.sort((a, b) => (+a)-(+b));
    
                var tmpLen = htmlControls.ddlPageSize.options.length - 1;
                while(tmpLen >= 0) { htmlControls.ddlPageSize.remove(tmpLen); tmpLen--; }
                for(var i = 0; i < allPageSizes.length; i++) {
                    htmlControls.ddlPageSize.add(new Option(allPageSizes[i], allPageSizes[i]));
                }
                htmlControls.ddlPageSize.options[allPageSizes.findIndex(s => s == pageSize)].selected = true;
                htmlControls.ddlPageSize.addEventListener("change", ddlPageSizeChanged);
            }
            htmlControls.btnFirst.addEventListener("click", function(){ pageNumber = 1; activateMe(); });
            htmlControls.btnPrev.addEventListener("click", function(){ pageNumber--; activateMe(); });
            htmlControls.btnNext.addEventListener("click", function(){ pageNumber++; activateMe(); });
            htmlControls.btnLast.addEventListener("click", function(){ pageNumber = totalPages; activateMe(); });
    
            function getQuickSearch(){
                var strQs = htmlControls.txtQuickSearch.value;
                
                return  strQs
                        ? cols.map(c => c.searchBy
                                ? c.searchBy.map(sb => sb + "*=*\"" + strQs + "\"").join("||")
                                : c.name + "*=*\"" + strQs + "\""
                            ).join("||")
                        : "";
            }
    
            function deleteItem(item){
                setLoading(true);
                $.ajax({
                    url: apiUrl + "/" + item.id
                    , type: "DELETE"
                }).then(function(data){
                    refreshData();
                    setLoading(false);
                }, function(jqXhr, textStatus, errorThrown){
                    if(jqXhr.responseJSON && jqXhr.responseJSON.errors && jqXhr.responseJSON.errors.id){
                        alert(jqXhr.responseJSON.errors.id);
                        setLoading(false);
                        return;
                    }
                    alert("fail... :-(");
                    setLoading(false);
                });
            }
            function viewItemHistFor(item){
                histList.activate(item.id);
            }
    
            function refreshData(){
                var userSearch = getQuickSearch();
                var searchString = userSearch;
                var listUrl = apiUrl;
                var listProgNameSuffix = "";
                if(strListType == "hist" && (+selectedId)){
                    listUrl += "/" + selectedId + "/History";
                    listProgNameSuffix += "History";
                }
                if(strListType == "deletedItems"){
                    listUrl += "/DeletedItems";
                    listProgNameSuffix += "DeletedItems";
                }
                console.log(strListType, listUrl);
                setLoading(true);

                $.ajax({
                    url: listSettingsApiUrl
                    , type: "POST"
                    , headers: {
                        'Accept': 'application/json'
                        , 'Content-Type': 'application/json'
                    }
                    , data: JSON.stringify({
                        listProgName: listProgName + "_" + listProgNameSuffix
                        , pageNumber: pageNumber
                        , pageSize: pageSize
                        , sortBy: sortedBy.map(sort => (sort.dir == "desc" ? "-" : "") + sort.sortBy).join(",")
                        , quickSearch: htmlControls.txtQuickSearch.value
                        , search: "" //used for advanced search screens only (not applicable here)
                    })
                    , dataType: "json"
                }).then(function(){
                    $.ajax({
                        url: listUrl
                        , data: {
                            start: (pageNumber - 1) * pageSize
                            , max: pageSize
                            , o: sortedBy.map(sort => (sort.dir == "desc" ? "-" : "") + sort.sortBy).join(",")
                            , f: searchString //the full search string as sent from the client side
                        }
                    }).then(function(data){
                        var firstItemIndex = data.firstItemIndex;
                        totalCount = (data.totalCount || 0);
                        totalPages = Math.ceil(totalCount / pageSize);
                        var items = data.items;
        
                        if(userSearch) cl.classList.add("active-search");
                        else cl.classList.remove("active-search");
        
                        if(totalCount == 0) {
                            cl.classList.add("no-records");
                            return;
                        }
                        
                        cl.classList.remove("no-records");
                        
                        pageNumber = Math.floor((firstItemIndex || 0) / pageSize) + 1;
                        correctDdlPageNumber();
                        correctDdlPageSize();
        
                        htmlControls.btnFirst.disabled = htmlControls.btnPrev.disabled = (pageNumber <= 1);
                        htmlControls.lblFrom.innerText = (firstItemIndex + 1);
                        htmlControls.lblTo.innerText = (firstItemIndex + items.length);
                        htmlControls.lblOutOf.innerText = totalCount;
                        htmlControls.btnNext.disabled = htmlControls.btnLast.disabled = (pageNumber >= totalPages);
        
                        var table = cl.querySelectorAll("table")[0];
                        var tbody = table.querySelectorAll("tbody")[0];
                        var newTBody = document.createElement("tbody");
                        items.forEach((item, ix) => {
                            var tr = document.createElement("tr");
                            if(ix % 2 == 1) { tr.className = "alt"; }
        
                            colCreators.forEach(colCreator => {
                                tr.appendChild(colCreator(item));
                            });
        
                            newTBody.appendChild(tr);
                        });
                        table.replaceChild(newTBody, tbody);
                        setLoading(false);
                    }, function(jqXhr, textStatus, errorThrown){
                        alert("fail... :-(");
                        setLoading(false);
                    });

                }, function(jqXhr, textStatus, errorThrown){
                    alert("fail... :-(");
                    setLoading(false);
                });
            }

            var strQs = urlQueryString.get("search") || listSettings.quickSearch;
            htmlControls.txtQuickSearch.value = strQs;
    
            function removeSearch(){
                htmlControls.txtQuickSearch.value = "";
                pageNumber = 1;
                activateMe();
            }
    
            htmlControls.btnRemoveQuickSearch.addEventListener("click", removeSearch);
            htmlControls.btnRemoveSearch.addEventListener("click", removeSearch);
            htmlControls.txtQuickSearch.addEventListener("keydown", (e) => {
                if(e.keyCode == 13) {
                    pageNumber = 1;
                    activateMe();
                }
            });
            
            htmlControls.btnQuickSearch.addEventListener("click", function(){ pageNumber = 1; activateMe(); });

            function activateMe(itemId){
                var objData = {
                    pag: pageNumber
                    , max: pageSize
                    , ord: sortedBy.map(sort => (sort.dir == "desc" ? "-" : "") + sort.label).join(",")
                    , search: htmlControls.txtQuickSearch.value
                };

                if(strListType == "hist"){
                    objData.id = (itemId || selectedId);
                }

                ({
                    "main": function(){ router.activate("?{pag}{max}{ord}{search}", strBaseTitle, objData); }
                    , "hist": function(){ router.activate("/{id}/History?{pag}{max}{ord}{search}", strBaseTitle + " - History", objData); }
                    , "deletedItems": function(){ router.activate("/DeletedItems?{pag}{max}{ord}{search}", strBaseTitle + " - Deleted Items", objData); }
                })[strListType]();
            }

            var list = {
                element: cl
                , show: function(objData){
                    cl.style.display = "block";
                    var newTitle = strBaseTitle + { "main": "", "hist": " - History", "deletedItems": " - Deleted Items" }[strListType];
                    setScreenTitle(newTitle);
                    if(objData.id){
                        selectedId = objData.id;
                    }

                    _mainListShownOnce |= (strListType == "main");

                    refreshData();
                }
                , hide: function(){ cl.style.display = "none"; cl.querySelectorAll("tbody")[0].innerHTML = ""; }
                , activate: activateMe
            }
    
            return list;
        }
    });
}

function getVal(obj){
    if(!obj) return null;

    switch (obj.tagName) {
        case "TEXTAREA":
        case "INPUT":
            if(obj.type.toLowerCase() == "checkbox" || obj.type.toLowerCase() == "radio"){
                var parent = obj.parentNode;
                while(parent && parent.tagName != "FORM"){
                    parent = parent.parentNode;
                }
                if(!parent) return obj.value;
                
                var sel = [];
                var foundCheckboxes = Array.prototype.filter.call(parent.querySelectorAll('input[name="' + obj.name + '"]'), function(input){ return !input.type || input.type.toLowerCase() != "hidden"} );
                if(foundCheckboxes.length > 1){
                    foundCheckboxes.forEach(chk => {
                        if(chk.checked){
                            sel.push(chk.value);
                        }
                    });
                }
                else {
                    return foundCheckboxes[0].checked;
                }
                return sel;
            }
            else return obj.value;
        case "SELECT":
            return obj.options[obj.selectedIndex].value;
    }

    return null;
}

function setVal(obj, newVal){
    if(!obj) return null;

    switch (obj.tagName) {
        case "TEXTAREA":
        case "INPUT":
            if(obj.type.toLowerCase() == "checkbox" || obj.type.toLowerCase() == "radio"){
                if(Array.isArray(newVal)){
                    obj.checked = (newVal.some(o => o == obj.value ));
                }
                else {
                    obj.checked = (obj.value == newVal) || newVal === true;
                }

                return newVal;
            }
            else return obj.value = newVal;
        case "SELECT":
            for(var ix = 0; ix < obj.options.length; ix++){
                if(obj.options[ix].value == newVal){
                    obj.options[ix].selected = true;
                    obj.selectedIndex = ix;
                }
            }
    }

    return null;
}