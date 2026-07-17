using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UTNGOL.Seguro.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            // SIMULACIÓN: Aquí luego irá la lógica que consuma la API de tus compañeros
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, (Email == "admin@utn.edu.ec") ? "Administrador" : "Registrado")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        // Para cargar la página
        public IActionResult Register()
        {
            return View();
        }

        // Para procesar los datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string Email, string Password, string ConfirmPassword)
        {
            // Lógica de simulación
            if (Password == ConfirmPassword)
            {
                return RedirectToAction("Index", "Home"); // Éxito
            }
            return View(); // Error, recarga la página
        }
    }
}