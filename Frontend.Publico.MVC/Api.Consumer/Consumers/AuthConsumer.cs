using System.Net.Http.Json;
using System.Diagnostics; // Necesario para Debug.WriteLine
using UTNGOL.Models;
using Api.Consumer.Config;

namespace Api.Consumer.Consumers
{
    public class AuthConsumer
    {
        private readonly HttpClient _httpClient;

        public AuthConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{ApiConfig.Estadisticas}/login",
                loginDto);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var mensajeError = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("EL SERVIDOR DICE: " + mensajeError);
            }

            return response.IsSuccessStatusCode;
        }
        public async Task<HttpResponseMessage> RegisterAsyncRaw(UserInputDTO userInputDto)
        {
            return await _httpClient.PostAsJsonAsync(
    $"{ApiConfig.Estadisticas}/usuarios/registrar",
    userInputDto);
        }
    }
}