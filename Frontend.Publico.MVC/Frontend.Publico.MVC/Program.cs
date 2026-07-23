using Api.Consumer.Consumers;
using System.Globalization;

namespace Frontend.Publico.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==========================
            // AUTENTICACIÓN
            // ==========================
            builder.Services
                .AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.Cookie.Name = "UTNGOL.Cookie";
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });

            // ==========================
            // MVC
            // ==========================
            builder.Services.AddControllersWithViews();

            // ==========================
            // SESSION
            // ==========================
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // ==========================
            // HTTP CLIENTS
            // ==========================
            builder.Services.AddHttpClient<AuthConsumer>();

            builder.Services.AddHttpClient<EstadisticasConsumer>();

            builder.Services.AddHttpClient<GolCoinConsumer>();

            builder.Services.AddHttpClient<PredictionConsumer>();

            var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}