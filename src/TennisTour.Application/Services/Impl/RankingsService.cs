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
            var allNewFinished = await _tournamentEditionRepository.GetAllFinishedAfterLastUpdate();
            await CalculateNewPoints(allNewFinished);
            await UpdateRankings();
        }

        private async Task UpdateRankings()
        {
            var allRankings = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByPoints();
            var i = 1;
            foreach (var ranking in allRankings)
            {
                ranking.PreviousRank = ranking.Rank;
                ranking.Rank = i;
                if (!ranking.BestRank.HasValue || ranking.BestRank < ranking.Rank)
                {
                    ranking.BestRank = ranking.Rank;
                    ranking.BestRankDate = DateTime.Now;
                }
                i++;
            }
            foreach (var ranking in allRankings)
            {
                await _rankingRepository.UpdateAsync(ranking);
            }
        }

        private async Task CalculateNewPoints(IList<TournamentEdition> editions)
        {
            var allRankChanges = editions.Select(e => e.Matches).SelectMany(list => list).Select(e => new RankChangeModel(e.WinnerId, e.Round, e.TournamentEdition.Tournament.Series));
            var allContenderIdsChangedSet = new HashSet<string>(allRankChanges.Select(e => e.ContenderId));
            var allNeededRankings = await _rankingRepository.GetAllOfContenderIds(allContenderIdsChangedSet.ToList());
            var updatedRankings = allNeededRankings.Select(e =>
            {
                e.PreviousPoints = e.Points;
                e.Points += allRankChanges.Where(it => it.ContenderId == e.ContenderId).Select(it => PointsCalculator.GetPoints(it.Series, it.Round)).Sum();
                return e;
            });
            foreach (var ranking in updatedRankings)
            {
                await _rankingRepository.UpdateAsync(ranking);
            }

            var allNewContenderIdsSetChanged = new HashSet<string>(allRankChanges.Select(e => e.ContenderId));
            var allNewContenderIdsSetWithoutRanking = allContenderIdsChangedSet.Except(allNewContenderIdsSetChanged);

            foreach(var contenderIdToAdd in  allNewContenderIdsSetWithoutRanking)
            {
                var contender = await _contenderInfoRepository.GetContenderInfoWithRankingByContenderIdAsync(contenderIdToAdd);
                await _rankingRepository.AddAsync(new Ranking
                {
                    Contender = contender.Contender,
                    ContenderId = contender.ContenderId,
                    Points =  allRankChanges.Where(it => it.ContenderId == contender.ContenderId).Select(it => PointsCalculator.GetPoints(it.Series, it.Round)).Sum()
                }) ;
            }
        } 
    }
}
