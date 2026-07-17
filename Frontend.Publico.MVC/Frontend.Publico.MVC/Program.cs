using UTNGOL.Servicios.Interface;
using UTNGOL.Servicios.Services;

namespace Frontend.Publico.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configuración de Autenticación
            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options => {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                });

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IEstadisticasService, MockEstadisticasService>();

            var app = builder.Build();

            // 2. Pipeline de Peticiones (Orden estricto)
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Importante para tus CSS y JS

            app.UseRouting();

            // ¡IMPORTANTE!: Este orden es lo que permite que el Login funcione
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}