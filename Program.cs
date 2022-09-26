using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoProject;
using DemoProject.Data;
using DemoProject.Data.Common;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//--------------------------------------------------
//identity types
builder.Services.AddIdentity<AppUser?, AppRole?>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddDefaultTokenProviders();

//identity services
builder.Services.AddTransient<IUserStore<AppUser?>, AppUserStore>();
builder.Services.AddTransient<IRoleStore<AppRole?>, AppRoleStore>();

// ProtectedData
builder.Services.AddDataProtection();

builder.Services.AddTransient<UserResolverService>();

// Add services to the container.
if(!string.IsNullOrEmpty(builder.Configuration.GetSection(nameof(EmailSender)).GetValue<string>("host"))){
    //could have used a service factory instead of this if/else and move the if in it
    builder.Services.AddTransient<IEmailSender, EmailSender>();
}
else {
    builder.Services.AddTransient<IEmailSender, EmailSender.ConsoleEmailSenderMock>();
}

builder.Services.AddTransient<DemoProject.Controllers.ListSettingsForUser>();

builder.Services.AddRazorPages(options => {
    options.Conventions.AddFolderRouteModelConvention("/", model =>
       {
           var selectorCount = model.Selectors.Count;
           for (var i = 0; i < selectorCount; i++)
           {
               var selector = model.Selectors[i];
               model.Selectors.Add(new SelectorModel
               {
                   AttributeRouteModel = new AttributeRouteModel
                   {
                       Order = 1,
                       Template = AttributeRouteModel.CombineTemplates(
                           selector.AttributeRouteModel!.Template,
                           "{0?}/{1?}/{2?}/{3?}/{4?}/{5?}/{6?}/{7?}/{8?}/{9?}"),
                   }
               });
           }
       });
});


#region [ OAuth2.0 3rd party providers setup (if applicable) ]
var authBuilder = builder.Services.AddAuthentication();
(string Id, string Secret)? getAuthOptions(string optionsConfigPrefix){
    var Id = builder!.Configuration.GetValue<string>($"{optionsConfigPrefix}:ClientId");
    var Secret = builder.Configuration.GetValue<string>($"{optionsConfigPrefix}:ClientSecret");

    return string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Secret) ? null : (Id, Secret);
}

var fbAuthOptions = getAuthOptions("OAuth:Facebook");
if(fbAuthOptions != null){
    authBuilder.AddFacebook(facebookOptions =>{
        facebookOptions.AppId = fbAuthOptions.Value.Id;
        facebookOptions.AppSecret = fbAuthOptions.Value.Secret;
    });
}
var googleAuthOptions = getAuthOptions("OAuth:Google");
if(googleAuthOptions != null){
    authBuilder.AddGoogle(googleOptions =>{
        googleOptions.ClientId = googleAuthOptions.Value.Id;
        googleOptions.ClientSecret = googleAuthOptions.Value.Secret;
    });
}
var msAuthOptions = getAuthOptions("OAuth:Microsoft");
if(msAuthOptions != null){
    authBuilder.AddMicrosoftAccount(msOptions => {
        msOptions.ClientId = msAuthOptions.Value.Id;
        msOptions.ClientSecret = msAuthOptions.Value.Secret;
    });
}
#endregion [ OAuth 2 3rd party providers setup (or not) ]

builder.Services.AddAuthorization();

var app = builder.Build();

//bundle the client side controls on startup
var contentRootPath = builder.Environment.ContentRootPath; //the project itself
var webRootPath = builder.Environment.WebRootPath; //wwwroot

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapRazorPages();

app.Run();
