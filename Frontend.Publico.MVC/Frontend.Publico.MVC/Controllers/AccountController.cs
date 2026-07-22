using Api.Consumer.Consumers;
using Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthConsumer _authService;

        public AccountController(AuthConsumer authService)
        {
            _authService = authService;
        }


        // ===========================
        // LOGIN
        // ===========================

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine("===== LOGIN MVC =====");
            Console.WriteLine($"Email: '{model.Email}'");
            Console.WriteLine($"Password: '{model.Password}'");


            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Complete correctamente el formulario.";
                return RedirectToAction("Index", "Home");
            }


            try
            {
                var loginDto = new LoginDTO
                {
                    Email = model.Email,
                    Password = model.Password
                };


                var usuario = await _authService.LoginAsync(loginDto);


                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Name ?? ""),
                        new Claim(ClaimTypes.Email, usuario.Email ?? ""),
                        new Claim(ClaimTypes.Role, usuario.Role ?? "USER"),
                        new Claim("IdUser", usuario.IdUser.ToString())
                    };


                    var identity = new ClaimsIdentity(
                        claims,
                        "CookieAuth"
                    );


                    var principal = new ClaimsPrincipal(identity);


                    await HttpContext.SignInAsync(
                        "CookieAuth",
                        principal
                    );


                    HttpContext.Session.SetInt32(
                        "IdUser",
                        usuario.IdUser
                    );


                    HttpContext.Session.SetString(
                        "Nombre",
                        usuario.Name ?? ""
                    );


                    HttpContext.Session.SetString(
                        "Email",
                        usuario.Email ?? ""
                    );


                    HttpContext.Session.SetString(
                        "Role",
                        usuario.Role ?? "USER"
                    );


                    return RedirectToAction(
                        "Index",
                        "Dashboard"
                    );
                }


                TempData["Error"] =
                    "Correo o contraseña incorrectos.";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                TempData["Error"] =
                    ex.Message;
            }


            return RedirectToAction(
                "Index",
                "Home"
            );
        }



        // ===========================
        // REGISTRO
        // ===========================

        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            Console.WriteLine("===== REGISTRO MVC =====");

            Console.WriteLine($"Nombre: {model.Nombre}");
            Console.WriteLine($"Email: {model.Email}");
            Console.WriteLine($"Username: {model.Username}");
            Console.WriteLine($"Password: {model.Password}");



            if (!ModelState.IsValid)
            {
                TempData["Error"] =
                    "Complete correctamente el formulario.";

                return RedirectToAction(
                    "Index",
                    "Home"
                );
            }



            var dto = new UserInputDTO
            {
                Name = model.Nombre,

                Email = model.Email,

                Username =
                    string.IsNullOrWhiteSpace(model.Username)
                    ? model.Email
                    : model.Username,

                Password = model.Password,

                IdRole = 1,

                Active = true
            };



            try
            {

                // Crear usuario en backend Java
                var usuario =
                    await _authService.RegisterAsync(dto);



                if (usuario == null)
                {
                    TempData["Error"] =
                        "No se pudo crear el usuario.";

                    return RedirectToAction(
                        "Index",
                        "Home"
                    );
                }



                Console.WriteLine(
                    $"Usuario creado ID: {usuario.IdUser}"
                );


                /*
                 La creación de la wallet NO se hace aquí.

                 La regla del negocio:
                 "Todo usuario nuevo recibe 10 GolCoins"

                 debe manejarla el backend de GolCoin.
                */


                TempData["Success"] =
                    "Usuario registrado correctamente.";


                return RedirectToAction(
                    "Index",
                    "Home"
                );

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                TempData["Error"] =
                    ex.Message;

            }


            return RedirectToAction(
                "Index",
                "Home"
            );
        }




        // ===========================
        // LOGOUT
        // ===========================

        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear();


            await HttpContext.SignOutAsync(
                "CookieAuth"
            );


            return RedirectToAction(
                "Index",
                "Home"
            );
        }

    }
}