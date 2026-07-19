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
        // Definimos la base correcta según lo que funciona en tu API
        private readonly string _baseUrl = "http://192.168.100.138:8080/estadisticas-backend/api";

        public EstadisticasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PartidoDTO>> ObtenerPartidosAsync()
        {
            // Cambiamos "/matches" por "/partidos" para que coincida con tu @Path en Java
            return await _httpClient.GetFromJsonAsync<List<PartidoDTO>>($"{_baseUrl}/partidos") ?? new();
        }

        public async Task<List<PosicionDTO>> ObtenerTablaPosicionesAsync() =>
            await _httpClient.GetFromJsonAsync<List<PosicionDTO>>($"{_baseUrl}/posiciones") ?? new();

        public async Task<List<GrupoDTO>> ObtenerGruposAsync() =>
            await _httpClient.GetFromJsonAsync<List<GrupoDTO>>($"{_baseUrl}/grupos") ?? new();

        public async Task<List<SeleccionDTO>> ObtenerSeleccionesAsync() =>
            await _httpClient.GetFromJsonAsync<List<SeleccionDTO>>($"{_baseUrl}/selecciones") ?? new();
    }
}