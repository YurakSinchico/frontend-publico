using System.Text.Json.Serialization;

namespace UTNGOL.Servicios.DTOs
{
    public class SeleccionDTO
    {
        [JsonPropertyName("idTeam")]
        public int IdSeleccion { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; }

        [JsonPropertyName("fifaCode")]
        public string CodigoFifa { get; set; }

        [JsonPropertyName("group")]
        public string Grupo { get; set; }

        [JsonPropertyName("points")]
        public int Puntos { get; set; }

        [JsonPropertyName("matchesPlayed")]
        public int PartidosJugados { get; set; }

        [JsonPropertyName("confederation")]
        public string Confederacion { get; set; }
    }
}