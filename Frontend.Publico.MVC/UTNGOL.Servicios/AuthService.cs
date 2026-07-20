using System.Net.Http.Json;
using System.Diagnostics; // Necesario para Debug.WriteLine
using UTNGOL.Servicios.DTOs;

namespace UTNGOL.Servicios
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDto)
        {
            // La ruta correcta es "estadisticas-backend/api/login"
            var response = await _httpClient.PostAsJsonAsync("estadisticas-backend/api/login", loginDto);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var mensajeError = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("EL SERVIDOR DICE: " + mensajeError);
            }
            return response.IsSuccessStatusCode;
        }
        public async Task<HttpResponseMessage> RegisterAsyncRaw(UserInputDTO userInputDto)
        {
            // ELIMINA LAS LÍNEAS DE AUTHENTICATIONHEADERVALUE
            // Solo envía la petición limpia
            return await _httpClient.PostAsJsonAsync("estadisticas-backend/api/usuarios/registrar", userInputDto);
        }
    }
}