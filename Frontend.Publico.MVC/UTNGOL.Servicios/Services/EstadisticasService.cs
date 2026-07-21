using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UTNGOL.Servicios.DTOs;
using UTNGOL.Servicios.Interface;

namespace UTNGOL.Servicios.Services
{
    public class EstadisticasService : IEstadisticasService
    {
        private readonly HttpClient _httpClient;
        // La URL base apunta a tu máquina virtual Fedora con el contexto del despliegue
        private readonly string _baseUrl = "http://192.168.100.138:8080/estadisticas-backend/api";

        public EstadisticasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PartidoDTO>> ObtenerPartidosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PartidoDTO>>($"{_baseUrl}/partidos") ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con Estadísticas (Partidos): {ex.Message}");
                return new List<PartidoDTO>();
            }
        }

        public async Task<GrupoDTO> ObtenerTablaPosicionesAsync(int idGrupo)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GrupoDTO>($"{_baseUrl}/grupos/{idGrupo}/posiciones");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con Estadísticas (Posiciones): {ex.Message}");
                return null;
            }
        }

        public async Task<List<GrupoDTO>> ObtenerGruposAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<GrupoDTO>>($"{_baseUrl}/grupos") ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con Estadísticas (Grupos): {ex.Message}");
                return new List<GrupoDTO>();
            }
        }

        public async Task<List<SeleccionDTO>> ObtenerSeleccionesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<SeleccionDTO>>($"{_baseUrl}/selecciones") ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con Estadísticas (Selecciones): {ex.Message}");
                return new List<SeleccionDTO>();
            }
        }
        public async Task<HttpResponseMessage> GuardarPrediccionAsync(PrediccionDTO prediccion, string email, string password)
        {
            var authString = $"{email}:{password}";
            var base64Auth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authString));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/predicciones");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);
            request.Content = JsonContent.Create(prediccion);

            return await _httpClient.SendAsync(request);
        }
    }
}