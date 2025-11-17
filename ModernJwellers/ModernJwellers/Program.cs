using Microsoft.EntityFrameworkCore;
using ModernJwellers;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("MJDBConnection");
builder.Services.AddDbContext<MJDbContext>(q => q.UseNpgsql(conn));
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
