using Foodi.Core;
using Foodi.Core.Services;
using Foodi.Infrastructure;
using Foodi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Foodi.Core.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Foodi.Repository.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Google;
using Stripe;
using DinkToPdf.Contracts;
using DinkToPdf;
using Foodi;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);


//connection to SQLServer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("No connection string was found");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<User,IdentityRole<int>>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddScoped<ApplicationDbContext>();


//confirm email
var smtpSettings = builder.Configuration.GetSection("Smtp"); 
builder.Services.AddSingleton<IEmailSender>(new EmailSender( 

    smtpSettings["MailServer"],
    int.Parse(smtpSettings["MailPort"]),
    smtpSettings["FromEmail"],
    smtpSettings["Password"]
));


//login with google
builder.Services.AddAuthentication().AddGoogle(Options =>
{
    IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
    Options.ClientId = googleAuthSection["ClientId"];
    Options.ClientSecret = googleAuthSection["ClientSecret"];

    Options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        var redirectUri = context.RedirectUri;
        context.Response.Redirect(redirectUri + "&prompt=select_account");
        return Task.CompletedTask;
    };
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoriesService,CategoriesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICartsService, CartsService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IOrdersService,OrdersService>();
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


//var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
//var wkHtmlToPdfPath = Path.Combine(builder.Environment.ContentRootPath, $"wkhtmltox\\v0.12.4\\{architectureFolder}\\libwkhtmltox");
//CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
//context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddTransient<Foodi.Services.PdfService>();



builder.Services.AddRazorPages();



// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        await SeedRoles.AddRoles(roleManager);
        await SeedUsers.AddAdmin(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
