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
        public const string Estadisticas =
            "http://192.168.51.131:8080/estadisticas-backend/api";

        public const string GolCoin =
            "http://192.168.51.41:57782/api";
    }
}
