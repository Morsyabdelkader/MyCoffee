using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.IRepository;
using MyCoffee.BL.Mapper;
using MyCoffee.BL.Repository;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });

});
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
builder.Services.AddScoped<IProductRep,ProductRep>();
builder.Services.AddScoped<IOrderRep, OrderRep>();
builder.Services.AddScoped<ICategoryRep, CategoryRep>();
//builder.Services.AddAuthentication()
//            .AddCookie(options =>
//  {
//      options.LoginPath = "/Account/SignIn"; // Customize the login path
//  })
//            .AddGoogle(option =>
//            {
//                IConfigurationSection configuration = builder.Configuration.GetSection("Google");
//                option.ClientId = configuration["ClientId"];
//                option.ClientSecret = configuration["ClientSecret"];
//                option.CallbackPath = "/signin-google";
//            });

//builder.Services.AddAuthentication().AddGoogle(option =>
//{
//    IConfigurationSection configuration = builder.Configuration.GetSection("Google");
//    option.ClientId = configuration["ClientId"];
//    option.ClientSecret = configuration["ClientSecret"];
//    option.CallbackPath = "/signin-google";

//});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    //options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option => {
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<DBContext>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
  .AddCookie(options =>
  {
      options.LoginPath = "/Account/SignIn"; // Customize the login path
  }).AddGoogle(option =>
{
    option.ClientId = builder.Configuration.GetSection("Google:ClientId").Value;
    option.ClientSecret = builder.Configuration.GetSection("Google:ClientSecret").Value;
   

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseStaticFiles();
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
