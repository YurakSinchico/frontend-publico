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
        private readonly string _baseUrl = "http://192.168.100.138:8080/api"; // Tu IP de Fedora

        public EstadisticasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PartidoDTO>> ObtenerPartidosAsync() =>
            await _httpClient.GetFromJsonAsync<List<PartidoDTO>>($"{_baseUrl}/partidos") ?? new();

        public async Task<List<PosicionDTO>> ObtenerTablaPosicionesAsync() =>
            await _httpClient.GetFromJsonAsync<List<PosicionDTO>>($"{_baseUrl}/posiciones") ?? new();

        public async Task<List<GrupoDTO>> ObtenerGruposAsync() =>
            await _httpClient.GetFromJsonAsync<List<GrupoDTO>>($"{_baseUrl}/grupos") ?? new();

        public async Task<List<SeleccionDTO>> ObtenerSeleccionesAsync()
        {
            // Construimos la URL completa basada en tu configuración de Java
            string url = "http://192.168.100.138:8080/estadisticas-backend/api/selecciones";

            return await _httpClient.GetFromJsonAsync<List<SeleccionDTO>>(url) ?? new List<SeleccionDTO>();
        }
    }
}