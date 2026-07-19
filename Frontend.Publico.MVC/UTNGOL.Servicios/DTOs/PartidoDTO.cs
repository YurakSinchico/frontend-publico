using System;
using System.Text.Json.Serialization; // Necesario para el mapeo

namespace UTNGOL.Servicios.DTOs
{
    public class PartidoDTO
    {
        [JsonPropertyName("idMatch")]
        public int IdPartido { get; set; }

        [JsonPropertyName("fifaMatchNumber")]
        public int NumeroPartidoFifa { get; set; }

        [JsonPropertyName("utcDateTime")]
        public DateTime FechaHoraUtc { get; set; }

        [JsonPropertyName("status")]
        public string Estado { get; set; }

        [JsonPropertyName("homeTeam")]
        public string SeleccionLocal { get; set; }

        [JsonPropertyName("awayTeam")]
        public string SeleccionVisitante { get; set; }

        [JsonPropertyName("homeGoals")]
        public int? GolesLocal { get; set; }

        [JsonPropertyName("awayGoals")]
        public int? GolesVisitante { get; set; }

        [JsonPropertyName("stadium")]
        public string Estadio { get; set; }

        [JsonPropertyName("group")]
        public string Grupo { get; set; }

        [JsonPropertyName("stage")]
        public string Fase { get; set; }
    }
}