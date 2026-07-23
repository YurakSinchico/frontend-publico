using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UTNGOL.Models
{
    public class PartidoDTO
    {
        [JsonPropertyName("idMatch")]
        public int IdPartido { get; set; }

        [JsonPropertyName("fifaMatchNumber")]
        public int? NumeroPartidoFifa { get; set; }

        [JsonPropertyName("matchDateTimeUtc")]
        public long? FechaHoraUtc { get; set; }

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

        [JsonPropertyName("venue")] 
        public string Estadio { get; set; }

        [JsonPropertyName("group")]
        public string Grupo { get; set; }

        [JsonPropertyName("phase")] 
        public string Fase { get; set; }

        // Propiedades explícitas para cuotas/odds comunes:
        [JsonPropertyName("homeOdds")]
        public decimal? HomeOdds { get; set; }

        [JsonPropertyName("drawOdds")]
        public decimal? DrawOdds { get; set; }

        [JsonPropertyName("awayOdds")]
        public decimal? AwayOdds { get; set; }

        [JsonPropertyName("cuotaLocal")]
        public decimal? CuotaLocal { get; set; }

        [JsonPropertyName("cuotaEmpate")]
        public decimal? CuotaEmpate { get; set; }

        [JsonPropertyName("cuotaVisitante")]
        public decimal? CuotaVisitante { get; set; }

        [JsonPropertyName("cuota1")]
        public decimal? Cuota1 { get; set; }

        [JsonPropertyName("cuotaX")]
        public decimal? CuotaX { get; set; }

        [JsonPropertyName("cuota2")]
        public decimal? Cuota2 { get; set; }

        [JsonPropertyName("cuota_local")]
        public decimal? CuotaLocalSnake { get; set; }

        [JsonPropertyName("cuota_empate")]
        public decimal? CuotaEmpateSnake { get; set; }

        [JsonPropertyName("cuota_visitante")]
        public decimal? CuotaVisitanteSnake { get; set; }

        [JsonPropertyName("cuota_1")]
        public decimal? Cuota1Snake { get; set; }

        [JsonPropertyName("cuota_x")]
        public decimal? CuotaXSnake { get; set; }

        [JsonPropertyName("cuota_2")]
        public decimal? Cuota2Snake { get; set; }

        [JsonPropertyName("oddsHome")]
        public decimal? OddsHome { get; set; }

        [JsonPropertyName("oddsDraw")]
        public decimal? OddsDraw { get; set; }

        [JsonPropertyName("oddsAway")]
        public decimal? OddsAway { get; set; }

        [JsonPropertyName("localOdds")]
        public decimal? LocalOdds { get; set; }

        [JsonPropertyName("empateOdds")]
        public decimal? EmpateOdds { get; set; }

        [JsonPropertyName("visitanteOdds")]
        public decimal? VisitanteOdds { get; set; }

        [JsonPropertyName("local")]
        public decimal? Local { get; set; }

        [JsonPropertyName("empate")]
        public decimal? Empate { get; set; }

        [JsonPropertyName("visitante")]
        public decimal? Visitante { get; set; }

        [JsonPropertyName("home")]
        public decimal? Home { get; set; }

        [JsonPropertyName("draw")]
        public decimal? Draw { get; set; }

        [JsonPropertyName("away")]
        public decimal? Away { get; set; }

        // Objeto anidado de cuotas en JSON (ej. "cuotas": { "empate": 1.0, ... } o "odds": { ... })
        [JsonPropertyName("cuotas")]
        public JsonElement? CuotasObj { get; set; }

        [JsonPropertyName("odds")]
        public JsonElement? OddsObj { get; set; }

        [JsonPropertyName("apuestas")]
        public JsonElement? ApuestasObj { get; set; }

        // Atrapalotodo para cualquier otra propiedad no mapeada en el JSON
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }

        public decimal GetHomeOdds()
        {
            if (HomeOdds.HasValue) return HomeOdds.Value;
            if (CuotaLocal.HasValue) return CuotaLocal.Value;
            if (Cuota1.HasValue) return Cuota1.Value;
            if (CuotaLocalSnake.HasValue) return CuotaLocalSnake.Value;
            if (Cuota1Snake.HasValue) return Cuota1Snake.Value;
            if (OddsHome.HasValue) return OddsHome.Value;
            if (LocalOdds.HasValue) return LocalOdds.Value;
            if (Local.HasValue) return Local.Value;
            if (Home.HasValue) return Home.Value;

            if (CuotasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("cuotas", CuotasObj.Value, new[] { "local", "home", "1", "cuota1" });
                if (val.HasValue) return val.Value;
            }
            if (OddsObj.HasValue)
            {
                var val = SearchJsonElementRecursive("odds", OddsObj.Value, new[] { "home", "local", "1", "odd1" });
                if (val.HasValue) return val.Value;
            }
            if (ApuestasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("apuestas", ApuestasObj.Value, new[] { "local", "home", "1" });
                if (val.HasValue) return val.Value;
            }

            var fromData = ExtractValueFromExtensionData("local", "home", "cuota1", "odd1", "cuota_1", "cuota_local", "home_odds", "local_odds");
            return fromData ?? 1.80m;
        }

        public decimal GetDrawOdds()
        {
            if (DrawOdds.HasValue) return DrawOdds.Value;
            if (CuotaEmpate.HasValue) return CuotaEmpate.Value;
            if (CuotaX.HasValue) return CuotaX.Value;
            if (CuotaEmpateSnake.HasValue) return CuotaEmpateSnake.Value;
            if (CuotaXSnake.HasValue) return CuotaXSnake.Value;
            if (OddsDraw.HasValue) return OddsDraw.Value;
            if (EmpateOdds.HasValue) return EmpateOdds.Value;
            if (Empate.HasValue) return Empate.Value;
            if (Draw.HasValue) return Draw.Value;

            if (CuotasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("cuotas", CuotasObj.Value, new[] { "empate", "draw", "x", "cuotax" });
                if (val.HasValue) return val.Value;
            }
            if (OddsObj.HasValue)
            {
                var val = SearchJsonElementRecursive("odds", OddsObj.Value, new[] { "draw", "empate", "x", "oddx" });
                if (val.HasValue) return val.Value;
            }
            if (ApuestasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("apuestas", ApuestasObj.Value, new[] { "empate", "draw", "x" });
                if (val.HasValue) return val.Value;
            }

            var fromData = ExtractValueFromExtensionData("empate", "draw", "cuotax", "oddx", "cuota_x", "cuota_empate", "draw_odds", "empate_odds");
            return fromData ?? 3.20m;
        }

        public decimal GetAwayOdds()
        {
            if (AwayOdds.HasValue) return AwayOdds.Value;
            if (CuotaVisitante.HasValue) return CuotaVisitante.Value;
            if (Cuota2.HasValue) return Cuota2.Value;
            if (CuotaVisitanteSnake.HasValue) return CuotaVisitanteSnake.Value;
            if (Cuota2Snake.HasValue) return Cuota2Snake.Value;
            if (OddsAway.HasValue) return OddsAway.Value;
            if (VisitanteOdds.HasValue) return VisitanteOdds.Value;
            if (Visitante.HasValue) return Visitante.Value;
            if (Away.HasValue) return Away.Value;

            if (CuotasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("cuotas", CuotasObj.Value, new[] { "visitante", "away", "2", "cuota2" });
                if (val.HasValue) return val.Value;
            }
            if (OddsObj.HasValue)
            {
                var val = SearchJsonElementRecursive("odds", OddsObj.Value, new[] { "away", "visitante", "2", "odd2" });
                if (val.HasValue) return val.Value;
            }
            if (ApuestasObj.HasValue)
            {
                var val = SearchJsonElementRecursive("apuestas", ApuestasObj.Value, new[] { "visitante", "away", "2" });
                if (val.HasValue) return val.Value;
            }

            var fromData = ExtractValueFromExtensionData("visitante", "away", "cuota2", "odd2", "cuota_2", "cuota_visitante", "away_odds", "visitante_odds");
            return fromData ?? 2.50m;
        }

        private decimal? ExtractValueFromExtensionData(params string[] keywords)
        {
            if (AdditionalData == null) return null;

            foreach (var kvp in AdditionalData)
            {
                var found = SearchJsonElementRecursive(kvp.Key, kvp.Value, keywords);
                if (found.HasValue) return found.Value;
            }
            return null;
        }

        private decimal? SearchJsonElementRecursive(string keyName, JsonElement element, string[] keywords)
        {
            var keyLower = keyName.ToLowerInvariant();
            foreach (var kw in keywords)
            {
                if (keyLower == kw || keyLower.EndsWith(kw) || keyLower.Contains(kw))
                {
                    if (element.ValueKind == JsonValueKind.Number && element.TryGetDecimal(out var decVal))
                    {
                        return decVal;
                    }
                    if (element.ValueKind == JsonValueKind.String && decimal.TryParse(element.GetString(), out var strVal))
                    {
                        return strVal;
                    }
                }
            }

            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in element.EnumerateObject())
                {
                    var found = SearchJsonElementRecursive(prop.Name, prop.Value, keywords);
                    if (found.HasValue) return found.Value;
                }
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    var found = SearchJsonElementRecursive(keyName, item, keywords);
                    if (found.HasValue) return found.Value;
                }
            }

            return null;
        }
    }
}


