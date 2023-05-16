using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;
using TennisTour.Application.Models.TodoList;
using TennisTour.Application.Models.Tournament;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;
using TennisTour.Shared.Services;

namespace TennisTour.Application.Services.Impl
{
    public class TournamentService : ITournamentService
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository, IMapper mapper, IClaimService claimService)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<IEnumerable<TournamentResponseModel>> GetAllOrderedByNameWithTournamentEditionsAsync()
        {
            var tournaments = await _tournamentRepository.GetAllOrderedByNameWithTournamentEditionsAsync();

            return _mapper.Map<IEnumerable<TournamentResponseModel>>(tournaments);
        }

        public async Task<TournamentResponseModel> GetByIdWithTournamentEditionsAsync(Guid id)
        {
            var tournaments = await _tournamentRepository.GetByIdWithTournamentEditionsAsync(id);

            return _mapper.Map<TournamentResponseModel>(tournaments);
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetOneAsync(tl => tl.Id == id);

            return new BaseResponseModel
            {
                Id = (await _tournamentRepository.DeleteAsync(tournament)).Id
            };
        }
    }
}
