using System.Text.Json.Serialization;

namespace UTNGOL.Models
{
    public class PosicionDTO
    {
        [JsonPropertyName("idTeam")]
        public int IdSeleccion { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; }

        [JsonPropertyName("fifaCode")]
        public string CodigoFifa { get; set; }

        [JsonPropertyName("matchesPlayed")]
        public int PartidosJugados { get; set; }

        [JsonPropertyName("points")]
        public int Puntos { get; set; }

        [JsonPropertyName("wins")]
        public int Victorias { get; set; }

        [JsonPropertyName("draws")]
        public int Empates { get; set; }

        [JsonPropertyName("losses")]
        public int Derrotas { get; set; }

        [JsonPropertyName("goalsFor")]
        public int GolesFavor { get; set; }

        [JsonPropertyName("goalsAgainst")]
        public int GolesContra { get; set; }

        [JsonPropertyName("goalDifference")]
        public int DiferenciaGoles { get; set; }
    }
}