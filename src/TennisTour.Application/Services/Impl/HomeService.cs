using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Rankings;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl
{
    public class HomeService : IHomeService
    {
        private readonly IMapper _mapper;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly IRankingRepository _rankingRepository;

        public HomeService(IMapper mapper, ITournamentEditionRepository tournamentEditionRepository, IRankingRepository rankingRepository)
        {
            _mapper = mapper;
            _tournamentEditionRepository = tournamentEditionRepository;
            _rankingRepository = rankingRepository;
        }

        public async Task<HomePageResponseModel> GetHomeAsync()
        {
            return new HomePageResponseModel
            {
                TopTenRankedConenders = _mapper.Map<List<RankingsResponseModel>>(await _rankingRepository.GetTopTenRankingsWithContenderDataOrderedByPoints()),
                LastTenFinishedTournaments = _mapper.Map<List<TournamentEditionResponseModel>>(await _tournamentEditionRepository.GetLastTenFinishedOrderedByDateStartDescAsync())
            };
        }
    }
}
