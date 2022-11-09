using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RCL.Core.Identity.Proofing;
using RCL.Core.Identity.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAzureBlobStorageServices(options => options.ConnectionString = builder.Configuration["ConnectionStrings:Storage"]);
builder.Services.AddRCLCoreAuthTokenServices(options => builder.Configuration.Bind("AzureAd", options));
builder.Services.AddRCLCoreIdentityGraphServices();
builder.Services.AddRCLCoreIdentitySecurityGroupServices();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserAdmins", policy =>
       policy.Requirements.Add(new GroupsCheckRequirement(new string[] { "UserAdmins" })));
});

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddRCLCoreIdentityProofingServices(optionsApi => builder.Configuration.Bind("IdentityProofingApi", optionsApi));

builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
