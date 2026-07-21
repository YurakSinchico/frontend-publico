using System.Net.Http.Json;
using Api.Consumer.Config;
using UTNGOL.Models;

namespace Api.Consumer.Consumers
{
    public class PredictionConsumer
    {
        private readonly HttpClient _httpClient;

        public PredictionConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Crear una nueva predicción
        public async Task<HttpResponseMessage> CrearPrediccionAsync(CreatePredictionDTO dto)
        {
            return await _httpClient.PostAsJsonAsync(
                $"{ApiConfig.GolCoin}/Predictions",
                dto);
        }

        // Obtener las predicciones de un usuario
        public async Task<List<PredictionDTO>> ObtenerPrediccionesUsuarioAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<PredictionDTO>>(
                $"{ApiConfig.GolCoin}/Predictions/user/{userId}")
                ?? new List<PredictionDTO>();
        }
    }
}