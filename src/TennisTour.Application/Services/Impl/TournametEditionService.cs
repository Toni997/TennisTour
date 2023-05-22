using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;
using TennisTour.Shared.Services;

namespace TennisTour.Application.Services.Impl
{
    public class TournametEditionService : ITournamentEditionService
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public TournametEditionService(ITournamentEditionRepository tournamentEditionRepository, ITournamentRepository tournamentRepository, IMapper mapper, IClaimService claimService)
        {
            _tournamentEditionRepository = tournamentEditionRepository;
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<IEnumerable<TournamentEditionResponseModel>> GetAllOrderedByDateStartDescAsync(
            CancellationToken cancellationToken = default)
        {
            var tournamentEditions = await _tournamentEditionRepository.GetAllOrderedByDateStartDescAsync();

            return _mapper.Map<IEnumerable<TournamentEditionResponseModel>>(tournamentEditions);
        }

        public async Task<TournamentEditionWithMatchesResponseModel> GetByIdWithMatchesAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var tournamentEdition = await _tournamentEditionRepository.GetByIdWithMatchesAsync(id);

            return _mapper.Map<TournamentEditionWithMatchesResponseModel>(tournamentEdition);
        }

        public async Task<UpsertTournamentEditionResponseModel> CreateAsync(UpsertTournamentEditionModel upsertTournamentEditionModel,
            CancellationToken cancellationToken = default)
        {
            var tournamentExists = await _tournamentRepository.ExistsAsync(upsertTournamentEditionModel.TournamentId);

            if (!tournamentExists)
                throw new UnprocessableRequestException("Tournament does not exist");

            var tournamentEdition = _mapper.Map<TournamentEdition>(upsertTournamentEditionModel);

            return new UpsertTournamentEditionResponseModel
            {
                Id = (await _tournamentEditionRepository.AddAsync(tournamentEdition)).Id
            };
        }

        public async Task<UpsertTournamentEditionResponseModel> UpdateAsync(Guid id, UpsertTournamentEditionModel upsertTournamentEditionModel)
        {
            var tournamentEdition = await _tournamentEditionRepository.GetByIdAsync(id);

            _mapper.Map(upsertTournamentEditionModel, tournamentEdition);

            return new UpsertTournamentEditionResponseModel
            {
                Id = (await _tournamentEditionRepository.UpdateAsync(tournamentEdition)).Id
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // TODO check if any related matches exist before allowing delete

            var tournamentEdition = await _tournamentEditionRepository.GetOneAsync(tl => tl.Id == id);

            return new BaseResponseModel
            {
                Id = (await _tournamentEditionRepository.DeleteAsync(tournamentEdition)).Id
            };
        }
    }
}
