using Microsoft.AspNetCore.Authentication.Cookies;
using UTNGOL.Servicios.Interface;
using UTNGOL.Servicios.Services;

namespace Frontend.Publico.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("CookieAuth")
     .AddCookie("CookieAuth", options =>
     {
         options.Cookie.Name = "UTNGOL.Cookie";
         options.LoginPath = "/Account/Login";
         options.AccessDeniedPath = "/Account/AccessDenied";
         options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Sesión de 1 hora
     });
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<IEstadisticasService, EstadisticasService>();

            builder.Services.AddHttpClient<UTNGOL.Servicios.AuthService>(client =>
            {
                // Esta es la IP donde está tu API de Java
                client.BaseAddress = new Uri("http://192.168.100.138:8080/");
            });
            var app = builder.Build();

            // 2. Pipeline de Peticiones (Orden estricto)
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
        }
    }
}