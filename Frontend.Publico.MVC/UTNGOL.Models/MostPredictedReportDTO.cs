using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Models
{
    public class MostPredictedReportDTO
    {
        public int MatchId { get; set; }

        public int TotalPredictions { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
