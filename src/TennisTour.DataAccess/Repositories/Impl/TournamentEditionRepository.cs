﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace TennisTour.DataAccess.Repositories.Impl
{
    public class TournamentEditionRepository : BaseRepository<TournamentEdition>, ITournamentEditionRepository
    {
        public TournamentEditionRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<TournamentEdition, object> IncludesForGetOne(IQueryable<TournamentEdition> x)
        {
            return x.Include(x => x.Winner)
                    .Include(x => x.Tournament)
                    .Include(x => x.TournamentRegistrations)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.MatchSets)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.Winner)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.Winner.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.Winner.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender.ContenderInfo);
        }

        private IIncludableQueryable<TournamentEdition, object> IncludesForGetAll(IQueryable<TournamentEdition> x)
        {
            return x.Include(x => x.Winner)
                        .ThenInclude(x => x.ContenderInfo)
                    .Include(x => x.Winner)
                        .ThenInclude(x => x.Ranking)
                    .Include(x => x.Tournament);
        }

        private IIncludableQueryable<TournamentEdition, object> IncludesForContenderDetails(IQueryable<TournamentEdition> x, string contenderId)
        {
            return x.Include(x => x.Winner)
                    .Include(x => x.Tournament)
                    .Include(x => x.Matches
                                    .Where(m => m.ContenderOneId == contenderId || m.ContenderTwoId == contenderId)
                                    .OrderBy(m => m.Round))
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.MatchSets)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.Winner)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender.Ranking);
        }

        private IIncludableQueryable<TournamentEdition, object> IncludesForGeneratingRound(IQueryable<TournamentEdition> x)
        {
            return x.Include(x => x.Winner)
                    .Include(x => x.Tournament)
                    .Include(x => x.TournamentRegistrations)
                        .ThenInclude(x => x.Contender)
                    .Include(x => x.TournamentRegistrations)
                        .ThenInclude(x => x.Contender.ContenderInfo)
                    .Include(x => x.TournamentRegistrations)
                        .ThenInclude(x => x.Contender.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.Winner)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderOne.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ContenderTwo.Ranking)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender.ContenderInfo)
                    .Include(x => x.Matches)
                        .ThenInclude(x => x.ResultReportedByContender.Ranking);
        }

        private IOrderedQueryable<TournamentEdition> OrderBy(IQueryable<TournamentEdition> x)
        {
            return x.OrderByDescending(x => x.DateStart);
        }

        private IOrderedQueryable<TournamentEdition> OrderByUpdateDate(IQueryable<TournamentEdition> x)
        {
            return x.OrderByDescending(x => x.UpdatedOn);
        }

        public async Task<IList<TournamentEdition>> GetAllOrderedByDateStartDescAsync()
        {
            return await GetAllAsync(orderBy: OrderBy, includes: IncludesForGetAll);
        }

        public async Task<TournamentEdition> GetByIdWithMatchesAsync(Guid id)
        {
            return await GetOneAsync(x => x.Id == id, IncludesForGetOne);
        }

        public async Task<TournamentEdition> GetByIdForGeneratingRoundAsync(Guid id)
        {
            return await GetOneAsync(x => x.Id == id, IncludesForGeneratingRound);
        }

        public async Task<IList<TournamentEdition>> GetLastTenByContenderWithMatchesOrderedByDateStartDescAsync(string contenderId)
        {
            return await GetAllAsync(expression: x => x.Matches.Any(x => x.ContenderOneId == contenderId || x.ContenderTwoId == contenderId),
                orderBy: OrderBy,
                includes: q => IncludesForContenderDetails(q, contenderId),
                take: 10);
        }

        public async Task<IList<TournamentEdition>> GetLastTenFinishedOrderedByDateStartDescAsync()
        {
            return await GetAllAsync(expression: x => x.Winner != null,
                orderBy: OrderBy,
                includes: IncludesForGetAll,
                take: 10);
        }

        public async Task<int> GetCareerTotalTitlesByContender(string contenderId)
        {
            var careerTitles = await GetAllAsync(x => x.WinnerId == contenderId);
            return careerTitles.Count;
        }

        public async Task<IList<TournamentEdition>> GetAllFinishedAfterLastUpdate()
        {
            var orderedByUpdateDateTime = await GetAllAsync(orderBy: OrderByUpdateDate);
            var lastUpdate = orderedByUpdateDateTime.Any() ? orderedByUpdateDateTime.First().UpdatedOn : new DateTime();
            return await GetAllAsync(expression: x => x.DateEnd >= lastUpdate, includes: IncludesForGetAll);
        }
        
        public async Task<IList<TournamentEdition>> GetAllUnfinishedTournamentEditionsOrderedByDateStartAsc()
        {
            return await GetAllAsync(x => x.Winner == null, orderBy: x => x.OrderBy(x => x.DateStart), includes: IncludesForGetOne);
        }

        public async Task<IList<TournamentEdition>> GetLastEditionForEveryTournamentAsync()
        {
            var query = Query();
            query = Expression(x => x.Winner != null, query);
            query = Include(IncludesForGetOne, query);
            return await query.GroupBy(te => te.TournamentId)
            .Select(group => group.OrderByDescending(te => te.DateStart).FirstOrDefault())
            .ToListAsync();
        }
    }
}
