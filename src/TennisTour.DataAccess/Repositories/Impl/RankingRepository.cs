using Microsoft.EntityFrameworkCore.Query;
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
    public class RankingRepository : BaseRepository<Ranking>, IRankingRepository
    {
        public RankingRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<Ranking, object> IncludesContenderData(IQueryable<Ranking> x)
        {
            return x.Include(x => x.Contender).ThenInclude(x=> x.ContenderInfo);
        }

        private IOrderedQueryable<Ranking> OrderByRankingPoints(IQueryable<Ranking> x)
        {
            return x.OrderByDescending(x => x.Points);
        }

        public async Task<IList<Ranking>> GetAllRankingsWithContenderDataOrderedByPoints()
        {
            return await GetAllAsync(orderBy: OrderByRankingPoints, includes: IncludesContenderData);
        }

        public async Task<IList<Ranking>> GetTopTenRankingsWithContenderDataOrderedByPoints()
        {
            return await GetAllAsync(orderBy: OrderByRankingPoints, includes: IncludesContenderData, take: 10);
        }

        public async Task<IList<Ranking>> GetAllOfContenderIds(IList<string> contenderIds)
        {
            return await GetAllAsync(expression: e => contenderIds.Contains(e.ContenderId),includes: IncludesContenderData);
        }
    }
}
