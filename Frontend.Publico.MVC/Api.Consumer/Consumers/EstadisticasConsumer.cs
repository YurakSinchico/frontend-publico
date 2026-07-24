using Api.Consumer.Config;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using UTNGOL.Models;

namespace Api.Consumer.Consumers
{
    public class EstadisticasConsumer
    {
        private readonly HttpClient _httpClient;

        public EstadisticasConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

      
        // PARTIDOS
        
        public async Task<List<PartidoDTO>> ObtenerPartidosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PartidoDTO>>(
                    $"{ApiConfig.Estadisticas}/partidos")
                    ?? new List<PartidoDTO>();
            }
            catch
            {
                return new List<PartidoDTO>();
            }
        }

        public async Task<PartidoDTO?> ObtenerPartidoPorIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PartidoDTO>(
                    $"{ApiConfig.Estadisticas}/partidos/{id}");
            }
            catch
            {
                return null;
            }
        }

       
        // GRUPOS
        public async Task<List<GrupoDTO>> ObtenerGruposAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<GrupoDTO>>(
                    $"{ApiConfig.Estadisticas}/grupos")
                    ?? new List<GrupoDTO>();
            }
            catch
            {
                return new List<GrupoDTO>();
            }
        }

        public async Task<GrupoDTO?> ObtenerGrupoAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GrupoDTO>(
                    $"{ApiConfig.Estadisticas}/grupos/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<GrupoDTO?> ObtenerTablaPosicionesAsync(int idGrupo)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GrupoDTO>(
                    $"{ApiConfig.Estadisticas}/grupos/{idGrupo}/posiciones");
            }
            catch
            {
                return null;
            }
        }

        // SELECCIONES
        
        public async Task<List<SeleccionDTO>> ObtenerSeleccionesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<SeleccionDTO>>(
                    $"{ApiConfig.Estadisticas}/selecciones")
                    ?? new List<SeleccionDTO>();
            }
            catch
            {
                return new List<SeleccionDTO>();
            }
        }

        public async Task<SeleccionDTO?> ObtenerSeleccionAsync(string codigoFifa)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SeleccionDTO>(
                    $"{ApiConfig.Estadisticas}/selecciones/{codigoFifa}");
            }
            catch
            {
                return null;
            }
        }

 
        // PREDICCIONES
      

        public async Task<HttpResponseMessage> GuardarPrediccionAsync(
            PrediccionDTO prediccion,
            string email,
            string password)
        {
            var auth = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{email}:{password}"));

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{ApiConfig.Estadisticas}/predicciones");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            request.Content = JsonContent.Create(prediccion);

            return await _httpClient.SendAsync(request);
        }
    }
}