using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;
using TennisTour.DataAccess.Models;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl
{
    internal class RankingsService : IRankingsService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IContenderInfoRepository _contenderInfoRepository;
        private readonly IMapper _mapper;

        public RankingsService(IRankingRepository rankingRepository, IMapper mapper, IContenderInfoRepository contenderInfoRepository)
        {
            _rankingRepository = rankingRepository;
            _mapper = mapper;
            _contenderInfoRepository = contenderInfoRepository;
        }

        public async Task<List<RankingsModel>> GetAllRankings()
        {
            var result = await _rankingRepository.GetAllAsync();
            var models = result.Select(it => _mapper.Map<RankingsModel>(it)).ToList();
            var info = await _contenderInfoRepository.GetContenderInfosByContenderIds(models.Select(it => it.ContenderId).ToList());
            return models.Select(it =>
            {
                it.ContenderInfo = info[models.IndexOf(it)];
                return it;
            }).OrderBy(it => it.Rank).ToList();
        }
    }
}
