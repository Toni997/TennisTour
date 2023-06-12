using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Models
{
    public class BaseMatchSetModel
    {
        public int ContenderOneGamesCount { get; set; }
        public int ContenderTwoGamesCount { get; set; }
        public int? LoserTiebreakPoints { get; set; }
    }

    public class UpsertMatchSetModel : BaseMatchSetModel { }

    public class UpsertMatchSetsModel
    {
        public MatchWinner Winner { get; set; }
        public List<UpsertMatchSetModel> MatchSets { get; set; } = new List<UpsertMatchSetModel>();
    }

    public class MatchSetResponseModel : BaseMatchSetModel
    {
        public int Order { get; set; }

        public override string? ToString()
        {
            return $"{ContenderOneGamesCount}-{ContenderTwoGamesCount}{(LoserTiebreakPoints.HasValue ? $"({LoserTiebreakPoints.Value})" : "")}";
        }
    }
}
