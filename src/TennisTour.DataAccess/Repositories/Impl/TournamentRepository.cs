using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Models;
using TennisTour.DataAccess.Persistence;
using X.PagedList;

namespace TennisTour.DataAccess.Repositories.Impl
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<Tournament, object> Includes(IQueryable<Tournament> x)
        {
            return x.Include(x => x.TournamentEditions);
        }

        private IOrderedQueryable<Tournament> OrderBy(IQueryable<Tournament> x)
        {
            return x.OrderBy(x => x.Name);
        }

        public async Task<Tournament> GetByIdWithTournamentEditionsAsync(Guid id)
        {
            return await GetOneAsync(x => x.Id == id, Includes);
        }

        public async Task<IList<Tournament>> GetAllOrderedByNameWithTournamentEditionsAsync()
        {
            return await GetAllAsync(orderBy: OrderBy, includes: Includes);
        }
    }
}
