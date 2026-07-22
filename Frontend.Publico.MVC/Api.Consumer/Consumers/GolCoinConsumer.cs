using System.Net.Http.Json;
using Api.Consumer.Config;
using UTNGOL.Models;

namespace Api.Consumer.Consumers
{
    public class GolCoinConsumer
    {
        private readonly HttpClient _httpClient;


        public GolCoinConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        // ===========================
        // WALLET
        // ===========================

        public async Task<WalletDTO?> ObtenerWalletAsync(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WalletDTO>(
                    $"{ApiConfig.GolCoin}/Wallets/{userId}");
            }
            catch
            {
                return null;
            }
        }



        // ===========================
        // TRANSACCIONES
        // ===========================

        public async Task<List<TransactionDTO>> ObtenerTransaccionesAsync(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TransactionDTO>>(
                    $"{ApiConfig.GolCoin}/Wallets/{userId}/transactions")
                    ?? new List<TransactionDTO>();
            }
            catch
            {
                return new List<TransactionDTO>();
            }
        }



        // ===========================
        // PREDICCIONES
        // ===========================

        public async Task<List<PredictionDTO>> ObtenerPrediccionesAsync(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PredictionDTO>>(
                    $"{ApiConfig.GolCoin}/Predictions/user/{userId}")
                    ?? new List<PredictionDTO>();
            }
            catch
            {
                return new List<PredictionDTO>();
            }
        }



        public async Task<HttpResponseMessage> CrearPrediccionAsync(CreatePredictionDTO dto)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync(
                    $"{ApiConfig.GolCoin}/Predictions",
                    dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción HTTP en CrearPrediccionAsync: " + ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("No se pudo conectar con la API backend de GolCoin (" + ApiConfig.GolCoin + "). Verifique que el servicio esté ejecutándose.")
                };
            }
        }

        // ===========================
        // RANKING / TODAS LAS WALLETS
        // ===========================

        public async Task<List<WalletDTO>> ObtenerTodasLasWalletsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<WalletDTO>>(
                    $"{ApiConfig.GolCoin}/Wallets")
                    ?? new List<WalletDTO>();
            }
            catch
            {
                return new List<WalletDTO>();
            }
        }
    }
}