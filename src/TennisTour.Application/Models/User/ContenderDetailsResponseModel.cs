using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Match;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Core.Entities;
using TennisTour.Core.Enums;

namespace TennisTour.Application.Models.User
{
    public class ContenderDetailsResponseModel : BaseResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int WeightKg { get; set; }
        public int HeightCm { get; set; }
        public BackhandType BackhandType { get; set; }
        public Hand DominantHand { get; set; }
        public DateTime TurnedProOn { get; set; }
        public DateTime? RetiredOn { get; set; }
        public ContenderRankingResponseModel Contender { get; set; }
        public ICollection<TournamentEditionWithMatchesResponseModel> LastTournamentsPlayed { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public class H2HResponseModel
    {
        public ContenderH2HDetailsResponseModel ContenderOne { get; set; }
        public ContenderH2HDetailsResponseModel ContenderTwo { get; set; }
        public ICollection<H2HMatchResponseModel> HeadToHeadMatches { get; set; }
    }

    public class ContenderH2HDetailsResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int WeightKg { get; set; }
        public int HeightCm { get; set; }
        public BackhandType BackhandType { get; set; }
        public Hand DominantHand { get; set; }
        public DateTime TurnedProOn { get; set; }
        public DateTime? RetiredOn { get; set; }
        public int CareerTotalTitles { get; set; }
        public int CareerTotalWins { get; set; }
        public int CareerTotalLoses { get; set; }
        public int CareerH2HWins { get; set; }
        public RankingResponseModel Ranking { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetRetiredOnForH2HTable()
        {
            if (!RetiredOn.HasValue) return "No";
            return RetiredOn.Value.Year.ToString();
        }
    }

    public class ContenderInfoModel : BaseResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int WeightKg { get; set; }
        public int HeightCm { get; set; }
        public BackhandType BackhandType { get; set; }
        public Hand DominantHand { get; set; }
        public DateTime? TurnedProOn { get; set; }
        public DateTime? RetiredOn { get; set; }
    }
}
