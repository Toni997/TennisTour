using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Models.Match;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl
{
    public class ContenderInfoService : IContenderInfoService
    {
        private readonly IContenderInfoRepository _contenderInfoRepository;
        private readonly ITournamentEditionRepository _tournamentEditionRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ContenderInfoService(
            IContenderInfoRepository contenderInfoRepository,
            ITournamentEditionRepository tournamentEditionRepository,
            IMatchRepository matchRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _contenderInfoRepository = contenderInfoRepository;
            _tournamentEditionRepository = tournamentEditionRepository;
            _matchRepository = matchRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ContenderInfoModel>  EditContenderInfoAsync(ContenderInfoModel contenderInfo, ClaimsPrincipal claimsPrincipal)
        {
            var toUpdate = _mapper.Map<ContenderInfo>(contenderInfo);
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            toUpdate.ContenderId = user.Id;
         
            if (await _contenderInfoRepository.ExistsAsync(contenderInfo.Id))
            {
                var savedContenderInfo = await _contenderInfoRepository.GetByIdAsync(contenderInfo.Id);

                if (savedContenderInfo.ContenderId != user.Id)
                    throw new UnauthorizedException("You are not authorized to change this contender's info");

                toUpdate.Contender = user;
                var result = await _contenderInfoRepository.UpdateAsync(toUpdate);
                return _mapper.Map<ContenderInfoModel>(result);
            }
            // TODO this should be a post request
            else
            {
                var result =  await _contenderInfoRepository.AddAsync(toUpdate);
                await _userManager.AddToRoleAsync(user, Roles.Contender);
                return _mapper.Map<ContenderInfoModel>(result);
            }
        }

        public async Task<ContenderInfoModel> GetContenderInfoAsync(string contenderUsername)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoOfUsenameAsync(contenderUsername);
            var response = _mapper.Map<ContenderInfoModel>(contenderInfo);
            return response;
        }

        public async Task<ContenderDetailsResponseModel> GetContenderInfoByContenderIdAsync(string contenderId, string authenticatedUserId)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoWithRankingByContenderIdAsync(contenderId);
            var contederDetailsModel = _mapper.Map<ContenderDetailsResponseModel>(contenderInfo);

            var lastTournamentsPlayed = await _tournamentEditionRepository.GetLastTenByContenderWithMatchesOrderedByDateStartDescAsync(contenderId);
            contederDetailsModel.LastTournamentsPlayed = _mapper.Map<List<TournamentEditionWithMatchesResponseModel>>(lastTournamentsPlayed);
            
            contederDetailsModel.Contender.IsFavoritedByUser = await _contenderInfoRepository.IsFavoritedByUser(contenderId, authenticatedUserId);
            
            return contederDetailsModel;
        }

        public async Task<H2HResponseModel> GetContendersH2HDetails(string contenderOneId, string contenderTwoId)
        {
            var contenderOne = await _contenderInfoRepository.GetContenderInfoWithRankingByContenderIdAsync(contenderOneId);
            var contenderTwo = await _contenderInfoRepository.GetContenderInfoWithRankingByContenderIdAsync(contenderTwoId);
            var h2hMatches = await _matchRepository.GetAllH2HMatchesBetweenContenderOneAndContenderTwo(contenderOneId, contenderTwoId);

            var h2hResponseModel = new H2HResponseModel
            {
                ContenderOne = _mapper.Map<ContenderH2HDetailsResponseModel>(contenderOne),
                ContenderTwo = _mapper.Map<ContenderH2HDetailsResponseModel>(contenderTwo),
                HeadToHeadMatches = _mapper.Map<List<H2HMatchResponseModel>>(h2hMatches)
            };

            h2hResponseModel.ContenderOne.CareerTotalTitles = await _tournamentEditionRepository.GetCareerTotalTitlesByContender(contenderOneId);
            h2hResponseModel.ContenderOne.CareerTotalWins = await _matchRepository.GetCareerTotalWinsByContender(contenderOneId);
            h2hResponseModel.ContenderOne.CareerTotalLoses = await _matchRepository.GetCareerTotalLosesByContender(contenderOneId);
            h2hResponseModel.ContenderOne.CareerH2HWins = await _matchRepository.GetCareerTotalH2HWinsByContenderOneAgainstContenderTwo(contenderOneId, contenderTwoId);

            h2hResponseModel.ContenderTwo.CareerTotalTitles = await _tournamentEditionRepository.GetCareerTotalTitlesByContender(contenderTwoId);
            h2hResponseModel.ContenderTwo.CareerTotalWins = await _matchRepository.GetCareerTotalWinsByContender(contenderTwoId);
            h2hResponseModel.ContenderTwo.CareerTotalLoses = await _matchRepository.GetCareerTotalLosesByContender(contenderTwoId);
            h2hResponseModel.ContenderTwo.CareerH2HWins = await _matchRepository.GetCareerTotalH2HWinsByContenderOneAgainstContenderTwo(contenderTwoId, contenderOneId);

            return h2hResponseModel;
        }
    }
}
