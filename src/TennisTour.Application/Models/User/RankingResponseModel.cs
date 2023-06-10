using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.User
{
    public class RankingResponseModel
    {
        public int Rank { get; set; }
        public int Points { get; set; }
        public int? PreviousRank { get; set; }
        public int? PreviousPoints { get; set; }
        public int? BestRank { get; set; }
        public DateTime? BestRankDate { get; set; }

        public string GetBestRank => $"{BestRank ?? Rank}";
        public string GetBestRankDate => BestRankDate?.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture) ?? "-";
    }
}
