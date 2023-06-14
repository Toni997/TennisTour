using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Repositories
{
    public interface IRankingRepository : IBaseRepository<Ranking>
    {
        public Task<IList<Ranking>> GetAllRankingsWithContenderDataOrderedByPoints();
        Task<IList<Ranking>> GetTopTenRankingsWithContenderDataOrderedByPoints();
        public Task<IList<Ranking>> GetAllOfContenderIds(IList<string> contenderIds);
    }
}
