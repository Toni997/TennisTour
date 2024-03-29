﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;
using TennisTour.Core.Models;

namespace TennisTour.Application.Models.Match
{
    public class MatchResponseModel : BaseResponseModel
    {
        public ContenderResponseModel ContenderOne { get; set; }
        public ContenderResponseModel ContenderTwo { get; set; }
        public ContenderResponseModel Winner { get; set; }
        public ContenderResponseModel ResultReportedByContender { get; set; }
        public bool IsResultConfirmed { get; set; }
        public int NextMatchupControlNumber { get; set; }
        public int Round { get; set; }

        public ICollection<MatchSetResponseModel> MatchSets { get; set; }

        public MarkupString GetMatchScore()
        {
            if (!MatchSets.Any())
            {
                return new MarkupString("No sets played");
            }
            var matchScoresList = new List<string>();
            foreach(var matchSet in MatchSets)
            {
                if (!matchSet.LoserTiebreakPoints.HasValue)
                {
                    matchScoresList.Add($"{matchSet.ContenderOneGamesCount}{matchSet.ContenderTwoGamesCount}");
                }
                else
                {
                    matchScoresList.Add($"{matchSet.ContenderOneGamesCount}{matchSet.ContenderTwoGamesCount}<sup>{matchSet.LoserTiebreakPoints.Value}</sup>");
                }
            }
            return new MarkupString("<span>" + string.Join(" ", matchScoresList) + "</span>");
        }
    }

    public class H2HMatchResponseModel
    {
        public int Round { get; set; }
        public H2HMatchWinnerResponseModel Winner { get; set; }
        public H2HMatchTournamentEditionResponseModel TournamentEdition { get; set; }
        public ICollection<MatchSetResponseModel> MatchSets { get; set; }
        public bool IsResultConfirmed { get; set; }

        public MarkupString GetMatchScore()
        {
            if (!MatchSets.Any())
            {
                return new MarkupString("No sets played");
            }
            var matchScoresList = new List<string>();
            foreach (var matchSet in MatchSets)
            {
                if (!matchSet.LoserTiebreakPoints.HasValue)
                {
                    matchScoresList.Add($"{matchSet.ContenderOneGamesCount}{matchSet.ContenderTwoGamesCount}");
                }
                else
                {
                    matchScoresList.Add($"{matchSet.ContenderOneGamesCount}{matchSet.ContenderTwoGamesCount}<sup>{matchSet.LoserTiebreakPoints.Value}</sup>");
                }
            }
            return new MarkupString("<span>" + string.Join(" ", matchScoresList) + "</span>");
        }
    }
}
