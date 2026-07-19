using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // Necesario para los atributos de mapeo

namespace UTNGOL.Servicios.DTOs
{
    public class GrupoDTO
    {
        [JsonPropertyName("idGroup")]
        public int IdGrupo { get; set; }

        [JsonPropertyName("code")]
        public string? Codigo { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; }

        [JsonPropertyName("standings")]
        public List<PosicionDTO> Posiciones { get; set; }
    }
}