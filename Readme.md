# README #

Make sure to modify appsettings.json, set the AppDbContextConnection to provide a valid sql server connection, the database may or may not exist, it will be created or updated by EF.

Optionally generate and set client ids and secrets for google, facebook, and/or  ms and update them in teh settings file.

Optionally add some email settings so that the system mail will work, otherwise only the console will show the email bodies.

From the project folder, run the command "dotnet ef database update" which will generate the database and some default data, assuming your current user has permissions to do that on the db server OR the connection string contains credentials that do.

From the same folder run "dotnet restore", then "dotnet run" and then click on one of the URLs logged by the app runner.

The existing default login is user: "admin@demo.test", pw: "ad-miniPa55" , all without quotes, you can register more too, permissions are not enforced in any way, you just need a login.

I hope I have managed to translate everything into english, remove code that didn't make sense now, and keep things functional.

### Interesting code: ###
    - /Pages/Administration/* (code is small but each file is a full module due to site.js and the backing rest API + conventions)
    - /wwwroot/js/site.js
    - /AppCode/* - contains a filter expression parser and classes to easily retrieve lists from linq (but the projection only properties cannot be searched, they need to be in the DB)
    - /Controllers/* - built to work with site.js
### May be of some interest: ###
    - /Program.cs, /EmailSender.cs, /UserResolverService.cs
### Not interesting (default code, libraries, scaffolded or similar): ###
    - everything else including:
    - /Migrations/*
    - /Pages/Account/*
    - /Pages/Error.* & /Pages/Index.*