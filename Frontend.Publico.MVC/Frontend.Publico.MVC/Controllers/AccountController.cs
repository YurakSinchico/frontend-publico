using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    // URL base correcta del proyecto en WildFly
    private readonly string _baseUrl = "http://192.168.0.12:8080/estadisticas-backend";

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        // La ruta final será /estadisticas-backend/usuarios/login
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/usuarios/login", new { Email, Password });

        if (response.IsSuccessStatusCode)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, Email) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home", new { error = "Credenciales incorrectas" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string Email, string Password, string ConfirmPassword, string Nombre)
    {
        if (Password != ConfirmPassword)
            return RedirectToAction("Register", new { error = "Las contraseñas no coinciden" });

        try
        {
            // LA CORRECCIÓN: La ruta es exactamente la que definiste en Java (@Path("/usuarios") + @Path("/registrar"))
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/usuarios/registrar", new { Email, Password, Nombre });

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home", new { mensaje = "¡Registro exitoso!" });
            }

            return RedirectToAction("Register", new { error = "El API devolvió error: " + response.StatusCode });
        }
        catch (Exception ex)
        {
            return RedirectToAction("Register", new { error = "Excepción de red: " + ex.Message });
        }
    }

    [HttpGet]
    public IActionResult Register() => View();

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}