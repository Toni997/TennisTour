using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;
using TennisTour.Core.Helpers;

namespace TennisTour.Application.Models.User
{
    public class H2HTableModel
    {
        public IEnumerable<H2HTableItemModel> H2HTableItems { get; set; }
        public H2HTableModel() { }

        public H2HTableModel(ContenderH2HDetailsResponseModel contenderOne, ContenderH2HDetailsResponseModel contenderTwo, TennisRules tennisRules)
        {
            var totalH2HMatches = contenderOne.CareerH2HWins + contenderTwo.CareerH2HWins;
            var contenderOneH2HPercentage = totalH2HMatches != 0 ? (int)Math.Round((double)(100 * contenderOne.CareerH2HWins) / totalH2HMatches) : 0;
            var contenderTwoH2HPercentage = totalH2HMatches != 0 ? 100 - contenderOneH2HPercentage : 0;

            var contenderOneTotalCareerMatches = contenderOne.CareerTotalWins + contenderOne.CareerTotalLoses;
            var contenderOneWinPercentage = contenderOneTotalCareerMatches != 0 ? (int)Math.Round((double)(100 * contenderOne.CareerTotalWins) / contenderOneTotalCareerMatches) : 0;

            var contenderTwoTotalCareerMatches = contenderTwo.CareerTotalWins + contenderTwo.CareerTotalLoses;
            var contenderTwoWinPercentage = contenderTwoTotalCareerMatches != 0 ? (int)Math.Round((double)(100 * contenderTwo.CareerTotalWins) / contenderTwoTotalCareerMatches) : 0;

            H2HTableItems = new List<H2HTableItemModel>
            {
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.ToString(),
                    SecondColumn = "VS",
                    ThirdColumn = contenderTwo.ToString()
                },
                new H2HTableItemModel
                {
                    FirstColumn = $"{tennisRules.CalculateContenderAge(contenderOne.DateOfBirth)} ({contenderOne.DateOfBirth.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)})",
                    SecondColumn = "Age",
                    ThirdColumn = $"{tennisRules.CalculateContenderAge(contenderTwo.DateOfBirth)} ({contenderTwo.DateOfBirth.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)})"
                },
                new H2HTableItemModel
                {
                    FirstColumn = $"{contenderOne.HeightCm} cm ({tennisRules.CentimetersToFeetString(contenderOne.HeightCm)})",
                    SecondColumn = "Height",
                    ThirdColumn = $"{contenderTwo.HeightCm} cm ({tennisRules.CentimetersToFeetString(contenderTwo.HeightCm)})"
                },
                new H2HTableItemModel
                {
                    FirstColumn = $"{contenderOne.WeightKg} kg ({tennisRules.KilogramsToPoundsString(contenderOne.WeightKg)})",
                    SecondColumn = "Weight",
                    ThirdColumn = $"{contenderTwo.WeightKg} kg ({tennisRules.KilogramsToPoundsString(contenderTwo.WeightKg)})"
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.DominantHand.GetDescription(),
                    SecondColumn = "Dominant Hand",
                    ThirdColumn = contenderTwo.DominantHand.GetDescription()
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.BackhandType.GetDescription(),
                    SecondColumn = "Backhand Type",
                    ThirdColumn = contenderTwo.BackhandType.GetDescription()
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.CareerTotalTitles.ToString(),
                    SecondColumn = "Career Titles",
                    ThirdColumn = contenderTwo.CareerTotalTitles.ToString()
                },
                new H2HTableItemModel
                {
                    FirstColumn = $"{contenderOne.CareerTotalWins}/{contenderOne.CareerTotalLoses} ({contenderOneWinPercentage}%)",
                    SecondColumn = "Career W/L",
                    ThirdColumn = $"{contenderTwo.CareerTotalWins}/{contenderTwo.CareerTotalLoses} ({contenderTwoWinPercentage}%)"
                },
                new H2HTableItemModel
                {
                    FirstColumn = $"{contenderOne.CareerH2HWins} ({contenderOneH2HPercentage}%)",
                    SecondColumn = "H2H Wins",
                    ThirdColumn = $"{contenderTwo.CareerH2HWins} ({contenderTwoH2HPercentage}%)"
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.Ranking != null ? $"{contenderOne.Ranking.Rank} ({contenderOne.Ranking.Points} points)" : "Unranked",
                    SecondColumn = "Rank (points)",
                    ThirdColumn = contenderTwo.Ranking != null ? $"{contenderTwo.Ranking.Rank} ({contenderTwo.Ranking.Points} points)" : "Unranked"
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.Ranking != null ? $"{contenderOne.Ranking.GetBestRank} ({contenderOne.Ranking.GetBestRankDate})" : "Unranked",
                    SecondColumn = "Best Rank (achieved on)",
                    ThirdColumn = contenderTwo.Ranking != null ? $"{contenderTwo.Ranking.GetBestRank} ({contenderTwo.Ranking.GetBestRankDate})" : "Unranked"
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.TurnedProOn.Year.ToString(),
                    SecondColumn = "Turned Pro",
                    ThirdColumn = contenderTwo.TurnedProOn.Year.ToString()
                },
                new H2HTableItemModel
                {
                    FirstColumn = contenderOne.GetRetiredOnForH2HTable(),
                    SecondColumn = "Retired",
                    ThirdColumn = contenderTwo.GetRetiredOnForH2HTable()
                },
            };
        }
    }

    public class H2HTableItemModel
    {
        public string FirstColumn { get; set; }
        public string SecondColumn { get; set; }
        public string ThirdColumn { get; set; }
    }
}
