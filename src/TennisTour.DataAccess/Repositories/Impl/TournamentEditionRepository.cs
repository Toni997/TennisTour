﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;

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
                        .ThenInclude(x => x.Contender)
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
                        .ThenInclude(x => x.Winner.Ranking);
        }

        private IIncludableQueryable<TournamentEdition, object> IncludesForGetAll(IQueryable<TournamentEdition> x)
        {
            return x.Include(x => x.Winner)
                        .ThenInclude(x => x.ContenderInfo)
                    .Include(x => x.Winner)
                        .ThenInclude(x => x.Ranking)
                    .Include(x => x.Tournament);
        }


        private IOrderedQueryable<TournamentEdition> OrderBy(IQueryable<TournamentEdition> x)
        {
            return x.OrderByDescending(x => x.DateStart);
        }

        public async Task<IList<TournamentEdition>> GetAllOrderedByDateStartDescAsync()
        {
            return await GetAllAsync(orderBy: OrderBy, includes: IncludesForGetAll);
        }

        public async Task<TournamentEdition> GetByIdWithMatchesAsync(Guid id)
        {
            return await GetOneAsync(x => x.Id == id, IncludesForGetOne);
        }
    }
}
