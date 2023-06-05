using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;
using TennisTour.Core.Exceptions;

namespace TennisTour.Core.Helpers
{
    public class TennisRules
    {
        public const int BestOfThree = 3;
        public const int BestOfFive = 5;
        public const int MinimumPossibleGamesWonInSet = 0;
        public const int MaximumPossibleGamesWonInSet = 7;
        public const int MinimumPossibleTiebreakPointsWon = 0;
        public const int MinimumGamesNeededToWinSet = 6;
        public const int MaximumPossibleTotalGamesPlayedInSet = 13;
        public const int MinimumPossibleTotalGamesPlayedInSet = 0;
        public const int MinimumGamesWonDifferenceToWinSetBeforeTiebreak = 2;
        public const double InchesPerFoot = 12;
        public const double CentimetersPerInch = 2.54;
        public const double PoundsPerKilogram = 2.20462262185;

        public int GetBestOfSetsForTourSeries(Series series) => series switch
        {
            Series.TTChallenger => BestOfThree,
            Series.TT250 => BestOfThree,
            Series.TT500 => BestOfThree,
            Series.TTMasters1000 => BestOfThree,
            Series.TTFinals => BestOfThree,
            Series.TTGrandSlam => BestOfFive,
            _ => throw new ArgumentOutOfRangeException($"Series {series} does not exist")
        };

        public bool IsSetScoreValid(int contenderOneGamesCount, int contenderTwoGamesCount, int? loserTiebreakPoints, bool shouldCheckIfSetEnded)
        {
            var gamesCountHigh = contenderOneGamesCount > contenderTwoGamesCount ? contenderOneGamesCount : contenderTwoGamesCount;
            var gamesCountLow = gamesCountHigh == contenderOneGamesCount ? contenderTwoGamesCount : contenderOneGamesCount;

            if (!IsGamesWonInSetByContenderInValidRange(gamesCountHigh) ||
                !IsGamesWonInSetByContenderInValidRange(gamesCountLow) ||
                !IsTotalGamesPlayedInSetInValidRange(gamesCountHigh + gamesCountLow))
            {
                return false;
            }

            if (loserTiebreakPoints.HasValue && !IsLoserTiebreakPointsWonInValidRange(loserTiebreakPoints.Value))
            {
                return false;
            }

            var gamesCountDifference = gamesCountHigh - gamesCountLow;

            if (gamesCountHigh == MaximumPossibleGamesWonInSet && gamesCountDifference > MinimumGamesWonDifferenceToWinSetBeforeTiebreak)
            {
                return false;
            }

            if (loserTiebreakPoints.HasValue && !IsTiebreakNeededToEndSet(gamesCountHigh, gamesCountLow))
            {
                return false;
            }

            if (shouldCheckIfSetEnded && gamesCountDifference < MinimumGamesWonDifferenceToWinSetBeforeTiebreak && !loserTiebreakPoints.HasValue)
            {
                return false;
            }

            return true;
        }

        private bool IsGamesWonInSetByContenderInValidRange(int gamesWon)
        {
            return gamesWon >= MinimumPossibleGamesWonInSet && gamesWon <= MaximumPossibleGamesWonInSet;
        }

        private bool IsTotalGamesPlayedInSetInValidRange(int totalGamesPlayedInSet)
        {
            return totalGamesPlayedInSet >= MinimumPossibleTotalGamesPlayedInSet && totalGamesPlayedInSet <= MaximumPossibleTotalGamesPlayedInSet;
        }

        private bool IsLoserTiebreakPointsWonInValidRange(int loserTiebreakPointsWon)
        {
            return loserTiebreakPointsWon >= MinimumPossibleTiebreakPointsWon;
        }

        private bool IsTiebreakNeededToEndSet(int gamesCountHigh, int gamesCountLow)
        {
            return gamesCountHigh == MaximumPossibleGamesWonInSet && gamesCountLow == MinimumGamesNeededToWinSet;
        }

        public string GetRoundName(int round, int numberOfRounds)
        {
            if (round == numberOfRounds) return "Finals";
            else if (round == numberOfRounds - 1) return "Semi-Finals";
            else if (round == numberOfRounds - 2) return "Quarter-Finals";
            else return "Round of " + Math.Pow(2, numberOfRounds - round + 1);
        }

        public string CentimetersToFeetString(int centimeters)
        {
            var inches = centimeters / CentimetersPerInch;
            var feet = inches / InchesPerFoot;

            var feetPart = (int)feet;
            var inchesPart = (int)Math.Round((feet - feetPart) * InchesPerFoot);

            return $"{feetPart}'{inchesPart}\"";
        }

        public string KilogramsToPoundsString(int kilograms)
        {
            var pounds = kilograms * PoundsPerKilogram;

            return $"{(int)pounds} lb";
        }

        public int CalculateContenderAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            // Check if the birthdate has occurred this year
            if (birthdate > today.AddYears(-age))
                age--;

            return age;
        }
    }
}
