using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Models;
using TennisTour.Core.Entities;
using TennisTour.Core.Enums;
using TennisTour.Core.Helpers;
using TennisTour.Core.Models;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl
{
    public class MatchService : IMatchService
    {
        private readonly IMapper _mapper;
        private IMatchRepository _matchRepository;
        private ITournamentEditionRepository _tournamentEditionRepository;
        private TennisRules _tennisRules;

        public MatchService(IMapper mapper,
            IMatchRepository matchRepository,
            ITournamentEditionRepository tournamentEditionRepository,
            TennisRules tennisRules)
        {
            _mapper = mapper;
            _matchRepository = matchRepository;
            _tournamentEditionRepository = tournamentEditionRepository;
            _tennisRules = tennisRules;
        }

        public async Task<BaseResponseModel> ReportResult(Guid id, UpsertMatchSetsModel upsertMatchSetsModel, string authenticatedContenderId)
        {
            var match = await _matchRepository.GetByIdWithMatchSetsAndWinner(id);

            if (match.ContenderOneId != authenticatedContenderId && match.ContenderTwoId != authenticatedContenderId)
                throw new UnauthorizedException("You can't report result for matches you did not participate in");

            if (match.IsResultConfirmed)
                throw new UnprocessableRequestException("This match has already been concluded");

            if (!_tennisRules.AreMatchSetsValid(upsertMatchSetsModel.MatchSets, match.TournamentEdition.Tournament.Series,
                upsertMatchSetsModel.Winner, new Guid(authenticatedContenderId), new Guid(match.ContenderOneId), new Guid(match.ContenderTwoId)))
                throw new BadRequestException("Match sets are invalid");

            UpdateMatch(match, upsertMatchSetsModel, authenticatedContenderId);
            await _matchRepository.UpdateAsync(match);

            return new BaseResponseModel
            {
                Id = id,
            };
        }

        private void UpdateMatch(Match match, UpsertMatchSetsModel upsertMatchSetsModel, string authenticatedContenderId)
        {
            match.ResultReportedByContenderId = authenticatedContenderId;
            match.WinnerId = upsertMatchSetsModel.Winner == MatchWinner.You ? authenticatedContenderId :
                (match.ContenderOneId == authenticatedContenderId ? match.ContenderTwoId : match.ContenderOneId);

            match.MatchSets.Clear();
            var index = 0;
            foreach (var matchSetModel in upsertMatchSetsModel.MatchSets)
            {
                var matchSet = _mapper.Map<MatchSet>(matchSetModel);
                matchSet.Order = ++index;
                match.MatchSets.Add(matchSet);
            }
        }

        public async Task<BaseResponseModel> ConfirmResult(Guid id, string authenticatedContenderId)
        {
            var match = await _matchRepository.GetByIdWithMatchSetsAndWinner(id);

            if (match.ContenderOneId != authenticatedContenderId && match.ContenderTwoId != authenticatedContenderId)
                throw new UnauthorizedException("You can't report result for matches you did not participate in");

            if (match.IsResultConfirmed)
                throw new UnprocessableRequestException("This match has already been concluded");

            if (match.ResultReportedByContenderId == authenticatedContenderId)
                throw new UnprocessableRequestException("You can't confirm your own result report");

            match.IsResultConfirmed = true;
            await _matchRepository.UpdateAsync(match);

            if (match.Round == match.TournamentEdition.Tournament.NumberOfRounds)
            {
                match.TournamentEdition.WinnerId = match.WinnerId;
                await _tournamentEditionRepository.UpdateAsync(match.TournamentEdition);
            }

            return new BaseResponseModel { Id = id };
        }
    }
}
