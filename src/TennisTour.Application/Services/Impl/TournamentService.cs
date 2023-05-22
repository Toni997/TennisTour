using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;
using TennisTour.Application.Models.TodoItem;
using TennisTour.Application.Models.TodoList;
using TennisTour.Application.Models.Tournament;
using TennisTour.Core.Entities;
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

        public async Task<IEnumerable<TournamentResponseModel>> GetAllOrderedByNameAsync(
            CancellationToken cancellationToken = default)
        {
            var tournaments = await _tournamentRepository.GetAllOrderedByNameAsync();

            return _mapper.Map<IEnumerable<TournamentResponseModel>>(tournaments);
        }

        public async Task<TournamentWithEditionsResponseModel> GetByIdWithTournamentEditionsAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var tournaments = await _tournamentRepository.GetByIdWithTournamentEditionsAsync(id);

            return _mapper.Map<TournamentWithEditionsResponseModel>(tournaments);
        }

        public async Task<UpsertTournamentResponseModel> CreateAsync(UpsertTournamentModel upsertTournamentModel,
            CancellationToken cancellationToken = default)
        {
            var tournament = _mapper.Map<Tournament>(upsertTournamentModel);

            return new UpsertTournamentResponseModel
            {
                Id = (await _tournamentRepository.AddAsync(tournament)).Id
            };
        }

        public async Task<UpsertTournamentResponseModel> UpdateAsync(Guid id, UpsertTournamentModel upsertTournamentModel)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);

            _mapper.Map(upsertTournamentModel, tournament);

            return new UpsertTournamentResponseModel
            {
                Id = (await _tournamentRepository.UpdateAsync(tournament)).Id
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // TODO check if any tournament editions exist before allowing delete

            var tournament = await _tournamentRepository.GetOneAsync(tl => tl.Id == id);

            return new BaseResponseModel
            {
                Id = (await _tournamentRepository.DeleteAsync(tournament)).Id
            };
        }
    }
}
