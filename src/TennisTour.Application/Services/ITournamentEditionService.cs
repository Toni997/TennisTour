﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.TournamentRegistration;
using TennisTour.Application.Models.Match;

namespace TennisTour.Application.Services
{
    public interface ITournamentEditionService
    {
        Task<IEnumerable<TournamentEditionResponseModel>> GetAllOrderedByDateStartDescAsync(CancellationToken cancellationToken = default);
        Task<TournamentEditionWithMatchesForDetailsResponseModel> GetByIdWithMatchesAsync(Guid id, string authenticatedUserId, CancellationToken cancellationToken = default);
        Task<UpsertTournamentEditionResponseModel> CreateAsync(UpsertTournamentEditionModel upsertTournamentModel, CancellationToken cancellationToken = default);
        Task<UpsertTournamentEditionResponseModel> UpdateAsync(Guid id, UpsertTournamentEditionModel upsertTournamentModel);
        Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> RegisterAsync(Guid tournamentEditionId, string contenderId);
        Task<BaseResponseModel> UnregisterAsync(Guid tournamentEditionId, string contenderId);
        Task<IEnumerable<TournamentRegistrationForEditionResponseModel>> GetAllRegistrationsAsync(Guid id);
        Task<IEnumerable<MatchResponseModel>> GenerateRoundAsync(Guid id);
        Task<IEnumerable<TournamentEditionForScheduleResponseModel>> GetScheduleAsync(string userId);
    }
}
