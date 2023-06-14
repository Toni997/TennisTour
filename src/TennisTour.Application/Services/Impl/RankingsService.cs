using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;
using TennisTour.DataAccess.Models;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;

namespace TennisTour.Application.Services.Impl
{
    internal class RankingsService : IRankingsService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly IContenderInfoRepository _contenderInfoRepository;
        private readonly IMapper _mapper;

        public RankingsService(IRankingRepository rankingRepository, IMapper mapper, ITournamentEditionRepository tournamentEditionRepository, IContenderInfoRepository contenderInfoRepository)
        {
            _rankingRepository = rankingRepository;
            _mapper = mapper;
            _tournamentEditionRepository = tournamentEditionRepository;
            _contenderInfoRepository = contenderInfoRepository;
        }

        public async Task<List<RankingsResponseModel>> GetAllRankings()
        {
            var rankings = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByPoints();
            return _mapper.Map<List<RankingsResponseModel>>(rankings);
        }

        public async Task UpdatePoints()
        {
            var rankings = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByPoints();
            // reset all rankings before recalculation
            foreach (var ranking in rankings)
            {
                ranking.PreviousPoints = ranking.Points;
                ranking.PreviousRank = ranking.Rank;
                ranking.Points = 0;
            }
            var tournamentEditions = await _tournamentEditionRepository.GetLastEditionForEveryTournamentAsync();
            // go through every last edition of every tournament and give winners points
            foreach (var edition in tournamentEditions)
            {
                foreach (var match in edition.Matches)
                {
                    var ranking = rankings.FirstOrDefault(x => x.ContenderId == match.WinnerId) ?? await AddRankingForContender(rankings, match.WinnerId);
                    ranking.Points += PointsCalculator.GetPointsPerRoundForSeries(edition.Tournament.Series);
                }
            }
            // we loop through all rankings ordered by points and update rank to reflect new points
            var newRankings = rankings.OrderByDescending(x => x.Points);
            var index = 1;
            foreach (var ranking in newRankings)
            {
                ranking.PreviousRank = ranking.Rank;
                ranking.Rank = index++;
                var newBestRank = !ranking.BestRank.HasValue || ranking.Rank < ranking.BestRank;
                if (newBestRank)
                {
                    ranking.BestRank = ranking.Rank;
                    ranking.BestRankDate = DateTime.Now;
                }
                await _rankingRepository.UpdateAsync(ranking);
            }
        }

        private async Task<Ranking> AddRankingForContender(IList<Ranking> rankings, string contenderId)
        {
            var ranking = await _rankingRepository.AddAsync(new Ranking
            {
                ContenderId = contenderId,
                Points = 0,
                Rank = 0,
                PreviousPoints = 0
            });
            rankings.Add(ranking);
            return ranking;
        }
    }
}
