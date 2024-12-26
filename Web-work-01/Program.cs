using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web_work_01.Models;

var builder = WebApplication.CreateBuilder(args);

// 註冊 Cookie 認證
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";  // 設定登入路徑
                    options.LogoutPath = "/Home/login";    // 設定登出路徑
                    options.AccessDeniedPath = "/Home/Login";
                });

string a = builder.Configuration.GetConnectionString("ExamDatabase");

// 註冊資料庫上下文
builder.Services.AddDbContext<ExamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDatabase"))
);

// 註冊控制器和視圖
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

var app = builder.Build();

// 啟用身份驗證
app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

// 啟用授權
app.UseAuthorization();



// 設置路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Loginview}/{id?}"); // 預設路徑

app.MapControllerRoute(
    name: "api-login",
    pattern: "api/{controller=Login}/{action=login}/{id?}");

app.Run();

