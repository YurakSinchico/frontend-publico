using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Consumer.Config;
using UTNGOL.Models;

namespace Api.Consumer.Config
{
    public static class ApiConfig
    {
        // Backend Estadísticas (Java)
        public const string Estadisticas =
            "http://192.168.100.138:8080/estadisticas-backend/api";

        // Backend GolCoin (.NET)
        public const string GolCoin =
            "https://localhost:57781/api";
    }
}
