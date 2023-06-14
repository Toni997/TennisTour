using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Match;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.TournamentRegistration;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;
using TennisTour.Shared.Services;

namespace TennisTour.Application.Services.Impl
{
    public class TournametEditionService : ITournamentEditionService
    {
        private readonly IMapper _mapper;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentRegistrationRepository _tournamentRegistrationRepository;

        public TournametEditionService(ITournamentEditionRepository tournamentEditionRepository,
            ITournamentRepository tournamentRepository,
            IMapper mapper,
            ITournamentRegistrationRepository tournamentRegistrationRepository)
        {
            _tournamentEditionRepository = tournamentEditionRepository;
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
            _tournamentRegistrationRepository = tournamentRegistrationRepository;
        }

        public async Task<IEnumerable<TournamentEditionResponseModel>> GetAllOrderedByDateStartDescAsync(
            CancellationToken cancellationToken = default)
        {
            var tournamentEditions = await _tournamentEditionRepository.GetAllOrderedByDateStartDescAsync();

            return _mapper.Map<IEnumerable<TournamentEditionResponseModel>>(tournamentEditions);
        }

        public async Task<TournamentEditionWithMatchesForDetailsResponseModel> GetByIdWithMatchesAsync(Guid id,
            string authenticatedUserId, CancellationToken cancellationToken = default)
        {
            var tournamentEdition = await _tournamentEditionRepository.GetByIdWithMatchesAsync(id);

            var tournamentEditionModel = _mapper.Map<TournamentEditionWithMatchesForDetailsResponseModel>(tournamentEdition);
            tournamentEditionModel.IsAuthenticatedUserRegisteredToPlay =
                await _tournamentRegistrationRepository.IsContenderRegisteredForTournamentEdition(authenticatedUserId, id);
            return tournamentEditionModel;
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
            var tournamentEdition = await _tournamentEditionRepository.GetOneAsync(
                expression: tl => tl.Id == id, includes: q => q.Include(te => te.Matches));

            if (tournamentEdition.Matches.Any())
                throw new UnprocessableRequestException("Tournament editions with existing matches cannot be updated");

            _mapper.Map(upsertTournamentEditionModel, tournamentEdition);

            return new UpsertTournamentEditionResponseModel
            {
                Id = (await _tournamentEditionRepository.UpdateAsync(tournamentEdition)).Id
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tournamentEdition = await _tournamentEditionRepository.GetOneAsync(
                expression: tl => tl.Id == id, includes: q => q.Include(te => te.Matches));

            if (tournamentEdition.Matches.Any())
                throw new UnprocessableRequestException("Tournament editions with existing matches cannot be deleted");

            return new BaseResponseModel
            {
                Id = (await _tournamentEditionRepository.DeleteAsync(tournamentEdition)).Id
            };
        }

        public async Task<BaseResponseModel> RegisterAsync(Guid tournamentEditionId, string contenderId)
        {
            await _tournamentEditionRepository.GetByIdAsync(tournamentEditionId);

            var existingRegistration = await _tournamentRegistrationRepository.GetOneOrNullAsync(
                    x => x.TournamentEditionId == tournamentEditionId && x.ContenderId == contenderId);

            if (existingRegistration is not null)
                throw new UnprocessableRequestException("You have already registered to play in this tournament");

            var registration = await _tournamentRegistrationRepository.AddAsync(new TournamentRegistration
            {
                TournamentEditionId = tournamentEditionId,
                ContenderId = contenderId,
            });

            return new BaseResponseModel
            {
                Id = registration.Id
            };
        }

        public async Task<BaseResponseModel> UnregisterAsync(Guid tournamentEditionId, string contenderId)
        {
            await _tournamentEditionRepository.GetByIdAsync(tournamentEditionId);

            var registration = await _tournamentRegistrationRepository.GetOneAsync(
                    x => x.TournamentEditionId == tournamentEditionId && x.ContenderId == contenderId);

            await _tournamentRegistrationRepository.DeleteAsync(registration);

            return new BaseResponseModel
            {
                Id = registration.Id
            };
        }

        public async Task<IEnumerable<TournamentRegistrationForEditionResponseModel>> GetAllRegistrationsAsync(Guid id)
        {
            var registrations = await _tournamentRegistrationRepository.GetAllByTournamentEditionAsync(id);

            return _mapper.Map<List<TournamentRegistrationForEditionResponseModel>>(registrations);
        }

        public async Task<IEnumerable<MatchResponseModel>> GenerateRoundAsync(Guid id)
        {
            var tournamentEdition = await _tournamentEditionRepository.GetByIdForGeneratingRoundAsync(id);

            var roundToGenerate = tournamentEdition.Matches.Any() ? tournamentEdition.Matches.Max(x => x.Round) + 1 : 1;

            if (roundToGenerate == 1)
                GenerateDraw(tournamentEdition);
            else
                GenerateNextRound(tournamentEdition, roundToGenerate);

            await _tournamentEditionRepository.UpdateAsync(tournamentEdition);

            return _mapper.Map<List<MatchResponseModel>>(tournamentEdition.Matches);
        }

        private void GenerateDraw(TournamentEdition tournamentEdition)
        {
            var contendersNeeded = (int)Math.Pow(2, tournamentEdition.Tournament.NumberOfRounds);

            if (tournamentEdition.TournamentRegistrations.Count < contendersNeeded)
                throw new UnprocessableRequestException("Not enough registrations to generate draw");

            tournamentEdition.IsRegistrationTimeOver = true;
            var acceptedRegistrations = tournamentEdition.TournamentRegistrations.OrderBy(x => x.Contender.Ranking.Rank).Take(contendersNeeded);
            foreach (var registration in acceptedRegistrations)
                registration.IsAccepted = true;

            GenerateRoundOneMatches(tournamentEdition, acceptedRegistrations.Select(x => x.Contender));
        }

        private void GenerateNextRound(TournamentEdition tournamentEdition, int roundToGenerate)
        {
            if (roundToGenerate > tournamentEdition.Tournament.NumberOfRounds)
                throw new UnprocessableRequestException("All rounds have already been generated for this tournament");

            var previousRoundMatches = tournamentEdition.Matches.Where(x => x.Round == roundToGenerate - 1).OrderBy(x => x.NextMatchupControlNumber);
            if (tournamentEdition.Matches.Any(x => !x.IsResultConfirmed))
                throw new UnprocessableRequestException("All match results need to be confirmed before generating next round");

            GenerateMatchesForRound(tournamentEdition, previousRoundMatches, roundToGenerate);
        }

        private void GenerateRoundOneMatches(TournamentEdition tournamentEdition, IEnumerable<ApplicationUser> contenders)
        {
            var contenderIndex = 0;
            var matchesToGenerate = contenders.Count() / 2;
            for (var i = 0; i < matchesToGenerate; i++)
            {
                var match = new Match
                {
                    ContenderOneId = contenders.ElementAt(contenderIndex++).Id,
                    ContenderTwoId = contenders.ElementAt(contenderIndex++).Id,
                    Round = 1,
                    NextMatchupControlNumber = i,
                };
                tournamentEdition.Matches.Add(match);
            }
        }

        private void GenerateMatchesForRound(TournamentEdition tournamentEdition, IEnumerable<Match> previousRoundMatches, int round)
        {
            var controlNumber = 0;
            for (var i = 0; i < previousRoundMatches.Count(); i+=2)
            {
                var match = new Match
                {
                    ContenderOneId = previousRoundMatches.ElementAt(i).WinnerId,
                    ContenderTwoId = previousRoundMatches.ElementAt(i + 1).WinnerId,
                    Round = round,
                    NextMatchupControlNumber = controlNumber++,
                };
                tournamentEdition.Matches.Add(match);
            }
        }

        public async Task<IEnumerable<TournamentEditionForScheduleResponseModel>> GetScheduleAsync(string userId)
        {
            var tournamentEditions = await _tournamentEditionRepository.GetAllUnfinishedTournamentEditionsOrderedByDateStartAsc();

            return _mapper.Map<List<TournamentEditionForScheduleResponseModel>>(tournamentEditions, opt => opt.Items["userId"] = userId);
        }
    }
}
