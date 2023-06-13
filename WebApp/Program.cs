using System.Text;
using DAL.Contracts;
using DAL.EF;
using DAL.EF.Seeding;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAppUow, AppUow>();

builder.Services
    .AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer")!,
            ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience")!,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key")!)),
            ClockSkew = TimeSpan.Zero,
        };
    });


const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

/*// add automapper configurations
builder.Services.AddAutoMapper(
   
    typeof(Public.DTO.AutomapperConfig)
);*/



//======================================================================
var app = builder.Build();
//======================================================================

// Set up all the database stuff and seed initial data
SetupAppData(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();



static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment environment, IConfiguration configuration)
{
    // dispose not needed if USING statement is being used
    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();

    // get DATABASE
    using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

    if (context == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize DB Context");
    }

    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();


    if (userManager == null || roleManager == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize UserManager or RoleManager");
    }


    var logger = serviceScope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();
    if (logger == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize logger");
    }

    //test run
    if (context.Database.ProviderName!.Contains("InMemory"))
    {
        return;
    }


    if (configuration.GetValue<bool>("DataInit:DropDatabase"))
    {
        logger.LogWarning("Dropping database");
        AppDataInit.DropDatabase(context);
    }

    if (configuration.GetValue<bool>("DataInit:MigrateDatabase"))
    {
        logger.LogInformation("Migrating database");
        AppDataInit.MigrateDatabase(context);
    }

    if (configuration.GetValue<bool>("DataInit:SeedIdentity"))
    {
        logger.LogInformation("Seeding identity");
        AppDataInit.SeedIdentity(userManager, roleManager);
    }

    if (configuration.GetValue<bool>("DataInit:SeedData"))
    {
        logger.LogInformation("Seed app data");
        AppDataInit.SeedAppData(context);
    }
}

public partial class Program
{
}
