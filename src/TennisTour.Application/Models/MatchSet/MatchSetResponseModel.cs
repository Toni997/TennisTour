using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Application.Models.MatchSet
{
    public class MatchSetResponseModel : BaseResponseModel
    {
        public int ContenderOneGamesCount { get; set; }
        public int ContenderTwoGamesCount { get; set; }
        public int? LoserTiebreakPoints { get; set; }
        public int Order { get; set; }
    }
}
