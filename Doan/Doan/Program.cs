﻿using Doan.Data;
using Doan.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với chuỗi kết nối
var ConnectString = builder.Configuration.GetConnectionString("ConnectDB");
builder.Services.AddDbContext<ConnectDB>(options =>
{
    options.UseMySql(ConnectString, ServerVersion.AutoDetect(ConnectString)); // Cấu hình cho MySQL
});

// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor();  // Thêm dịch vụ IHttpContextAccessor

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Thời gian timeout của session
    options.Cookie.HttpOnly = true; // Cookie bảo mật
    options.Cookie.IsEssential = true; // Cookie cần thiết để hoạt động
});

// Thêm các dịch vụ cần thiết
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Home/Login"; // Đường dẫn trang đăng nhập
        options.AccessDeniedPath = "/Home/AccessDenied"; // Đường dẫn nếu không có quyền
    });

builder.Services.AddAuthorization();

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

// Kích hoạt Session middleware
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CategoryMiddleware>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
