using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Models
{
    public class UpsertMatchSetModel
    {
        public int ContenderOneGamesCount { get; set; }
        public int ContenderTwoGamesCount { get; set; }
        public int? LoserTiebreakPoints { get; set; }
    }

    public class UpsertMatchSetsModel
    {
        public MatchWinner Winner { get; set; }
        public List<UpsertMatchSetModel> MatchSets { get; set; } = new List<UpsertMatchSetModel>();
    }
}
