using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.Rankings
{
    public class RankingsResponseModel : BaseResponseModel
    {
        public ContenderInfoDto ContenderInfo { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int? PreviousRank { get; set; }
        public int? PreviousPoints { get; set; }
        public int? BestRank { get; set; }
        public DateTime? BestRankDate { get; set; }
    }

}
