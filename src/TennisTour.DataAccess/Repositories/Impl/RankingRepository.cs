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

        private IIncludableQueryable<Ranking, object> IncludesContenderData(IQueryable<Ranking> x)
        {
            return x.Include(x => x.Contender).ThenInclude(x=> x.ContenderInfo);
        }

        private IOrderedQueryable<Ranking> OrderByRankingPoints(IQueryable<Ranking> x)
        {
            return x.OrderByDescending(x => x.Points);
        }

        public async Task<IList<Ranking>> GetAllRankingsWithContenderDataOrderedByRank()
        {
            return await GetAllAsync(orderBy: OrderByRankingPoints, includes: IncludesContenderData);
        }
        public RankingRepository(DatabaseContext context) : base(context) { }
    }
}
