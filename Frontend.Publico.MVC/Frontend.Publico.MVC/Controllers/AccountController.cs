using Frontend.Publico.MVC.ViewModels;
using Frontend.Publico.MVC.ViewModels.Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication; // Asegúrate de añadir esto
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Y esto
using UTNGOL.Servicios;
using UTNGOL.Servicios.DTOs;

namespace Frontend.Publico.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // Mapeo a DTO
                var loginDto = new LoginDTO { Email = model.Email, Password = model.Password };

                // Llamada real al servicio
                bool esValido = await _authService.LoginAsync(loginDto);

                if (esValido)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Email) };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToAction("Index", "Partidos");
                }

                // Si llega aquí, es porque la API respondió "No autorizado" o "Error"
                ViewBag.Error = "Credenciales incorrectas o usuario no encontrado.";
            }
            catch (Exception ex)
            {
                // ESTA PARTE ES LA CLAVE PARA SABER QUÉ PASA
                System.Diagnostics.Debug.WriteLine("ERROR DE CONEXIÓN CON API: " + ex.Message);
                ViewBag.Error = "Error al conectar con el servidor. Verifica que la API esté encendida.";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Necesitas crear esta vista
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userDto = new UserInputDTO
            {
                Name = model.Nombre,
                Email = model.Email,
                Username = model.Username ?? model.Email,
                Password = model.Password,
                IdRole = 1,
                Active = true
            };

            try
            {
                // Llamamos al nuevo método que nos da la respuesta completa
                var response = await _authService.RegisterAsyncRaw(userDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }

                // AQUÍ CAPTURAMOS EL ERROR REAL DEL SERVIDOR
                var errorContent = await response.Content.ReadAsStringAsync();

                // Esto se mostrará en tu <div asp-validation-summary="All">
                ModelState.AddModelError("", "Error del servidor: " + errorContent);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se pudo conectar con el servidor: " + ex.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Partidos");
        }
    }
}