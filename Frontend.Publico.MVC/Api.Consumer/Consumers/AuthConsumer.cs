using System.Net.Http.Json;
using Api.Consumer.Config;
using UTNGOL.Models;

namespace Api.Consumer.Consumers
{
    public class AuthConsumer
    {
        private readonly HttpClient _httpClient;

        public AuthConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO?> LoginAsync(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{ApiConfig.Estadisticas}/login",
                loginDto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        public async Task<HttpResponseMessage> RegisterAsyncRaw(UserInputDTO dto)
        {
            return await _httpClient.PostAsJsonAsync(
                $"{ApiConfig.Estadisticas}/usuarios/registrar",
                dto);
        }
    }
}