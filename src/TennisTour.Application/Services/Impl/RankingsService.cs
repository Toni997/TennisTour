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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly IMapper _mapper;

        public RankingsService(IRankingRepository rankingRepository, IMapper mapper, ITournamentEditionRepository tournamentEditionRepository, UserManager<ApplicationUser> userManager)
        {
            _rankingRepository = rankingRepository;
            _mapper = mapper;
            _tournamentEditionRepository = tournamentEditionRepository;
            _userManager = userManager;
        }

        public async Task<List<RankingsResponseModel>> GetAllRankings()
        {
            var allLocal = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByPoints();
            var mapped = _mapper.Map<IEnumerable<RankingsResponseModel>>(allLocal).ToList();
            var infoDtos = allLocal.Select(e => _mapper.Map<ContenderInfoModel>(e.Contender.ContenderInfo)).ToList();
            return mapped.Select(e => {
                e.ContenderInfo = infoDtos[mapped.IndexOf(e)];
                return e;
            }).ToList();
         
        }

        public async Task<List<RankingsResponseModel>> UpdatePoints(ClaimsPrincipal claimsPrincipal)
        {
            var allNewFinished = await _tournamentEditionRepository.GetAllFinishedBeforeDate(startDate: DateTime.Now);
            await CalculateNewPoints(allNewFinished);
            var rankings = await UpdateRankings(claimsPrincipal);
            return _mapper.Map<IEnumerable<RankingsResponseModel>>(rankings).ToList();
        }
        private async Task<IList<RankingsResponseModel>> UpdateRankings(ClaimsPrincipal claimsPrincipal)
        {
            var allRankings = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByPoints();
            var i = 1;
            foreach (var ranking in allRankings)
            {
                ranking.PreviousRank = ranking.Rank;
                ranking.Rank = i;
                if (ranking.BestRank != null && ranking.BestRank < ranking.Rank)
                {
                    ranking.BestRank = ranking.Rank;
                }
                ranking.UpdatedOn = DateTime.Now;
                ranking.UpdatedBy = _userManager.GetUserId(claimsPrincipal);
                i++;
            }
            foreach (var ranking in allRankings)
            {
                await _rankingRepository.UpdateAsync(ranking);
            }
            return _mapper.Map<IEnumerable<RankingsResponseModel>>(allRankings).ToList();
        }
        private async Task CalculateNewPoints(IList<TournamentEdition> editions)
        {
            var allRankChanges = editions.Select(e => e.Matches).SelectMany(list => list).Select(e => new RankChangeModel(e.WinnerId, e.Round, e.TournamentEdition.Tournament.Series));
            var allNeededRankings = await _rankingRepository.GetAllOfContenderIds((new HashSet<string>(allRankChanges.Select(e => e.ContenderId))).ToList());
            var updatedRankings = allNeededRankings.Select(e =>
            {
                e.PreviousPoints = e.Points;
                e.Points += allRankChanges.Where(it => it.ContenderId == e.ContenderId).Select(it => PointsCalculator.GetPoints(it.Series, it.Round)).Sum();
                return e;
            });
            foreach(var ranking in updatedRankings)
            {
                await _rankingRepository.UpdateAsync(ranking);
            }
         

        } 
    }
}
