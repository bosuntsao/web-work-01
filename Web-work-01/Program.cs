using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web_work_01.Models;

var builder = WebApplication.CreateBuilder(args);

// ���U Cookie �{��
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";  // �]�w�n�J���|
                    options.LogoutPath = "/Home/login";    // �]�w�n�X���|
                    options.AccessDeniedPath = "/Home/Login";
                });

string a = builder.Configuration.GetConnectionString("ExamDatabase");

// ���U��Ʈw�W�U��
builder.Services.AddDbContext<ExamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDatabase"))
);

// ���U����M����
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

var app = builder.Build();

// �ҥΨ�������
app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

// �ҥα��v
app.UseAuthorization();



// �]�m����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Loginview}/{id?}"); // �w�]���|

app.MapControllerRoute(
    name: "api-login",
    pattern: "api/{controller=Login}/{action=login}/{id?}");

app.Run();

