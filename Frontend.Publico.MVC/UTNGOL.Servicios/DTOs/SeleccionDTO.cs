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

        // Agrega el resto de propiedades si las necesitas...
        [JsonPropertyName("confederation")] // Asegúrate de que el nombre entre comillas coincida con el JSON
        public string Confederacion { get; set; }
    }
}