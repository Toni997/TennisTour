using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.User
{
    public class ContenderResponseModel : BaseResponseModel
    {
        public RankingResponseModel Ranking { get; set; }
        public ContenderInfoModel ContenderInfo { get; set; }

        public override string? ToString()
        {
            return $"{ContenderInfo.FirstName} {ContenderInfo.LastName}";
        }

        public string GetCurrentRank()
        {
            return Ranking?.Rank.ToString() ?? "Unranked";
        }

        public string GetCurrentPoints()
        {
            return Ranking?.Points.ToString() ?? "Unranked";
        }
    }

    public class H2HMatchWinnerResponseModel : BaseResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string? ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public class ContenderWithRankingResponseModel : BaseResponseModel
    {
        public RankingResponseModel Ranking { get; set; }
        public int FavoritedByUsersCount { get; set; }
        public bool IsFavoritedByUser { get; set; }
    }

    public class ContenderWithRankingForFavoritesResponseModel : BaseResponseModel
    {
        public RankingResponseModel Ranking { get; set; }
    }
}
