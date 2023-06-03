using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;
using TennisTour.Application.Models.User;
using TennisTour.DataAccess.Models;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl
{
    internal class RankingsService : IRankingsService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IMapper _mapper;

        public RankingsService(IRankingRepository rankingRepository, IMapper mapper)
        {
            _rankingRepository = rankingRepository;
            _mapper = mapper;
        }

        public async Task<List<RankingsResponseModel>> GetAllRankings()
        {
            var allLocal = await _rankingRepository.GetAllRankingsWithContenderDataOrderedByRank();
            var mapped = _mapper.Map<IEnumerable<RankingsResponseModel>>(allLocal).ToList();
            var infoDtos = allLocal.Select(e => _mapper.Map<ContenderInfoModel>(e.Contender.ContenderInfo)).ToList();
            return mapped.Select(e => {
                e.ContenderInfo = infoDtos[mapped.IndexOf(e)];
                return e;
            }).ToList();
         
        }
    }
}
