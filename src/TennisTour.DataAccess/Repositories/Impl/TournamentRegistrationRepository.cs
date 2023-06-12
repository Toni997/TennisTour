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
    public class TournamentRegistrationRepository : BaseRepository<TournamentRegistration>, ITournamentRegistrationRepository
    {
        public TournamentRegistrationRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<TournamentRegistration, object> Includes(IQueryable<TournamentRegistration> x)
        {
            return x.Include(x => x.Contender)
                        .ThenInclude(x => x.ContenderInfo)
                    .Include(x => x.Contender)
                        .ThenInclude(x => x.Ranking);
        }

        private IOrderedQueryable<TournamentRegistration> OrderBy(IQueryable<TournamentRegistration> x)
        {
            return x.OrderByDescending(x => x.IsAccepted)
                    .ThenBy(x => x.Contender.ContenderInfo.FirstName)
                    .ThenBy(x => x.Contender.ContenderInfo.LastName);
        }

        public async Task<bool> IsContenderRegisteredForTournamentEdition(string contenderId, Guid tournamentEditionId)
        {
            var registration = await GetOneOrNullAsync(x => x.ContenderId == contenderId && x.TournamentEditionId == tournamentEditionId);
            return registration is not null;
        }

        public async Task<IList<TournamentRegistration>> GetAllByTournamentEditionAsync(Guid tournamentEditionId)
        {
            return await GetAllAsync(x => x.TournamentEditionId == tournamentEditionId,
                includes: Includes, orderBy: OrderBy);
        }

        public async Task<IList<TournamentRegistration>> GetAllAcceptedByTournamentEditionAsync(Guid tournamentEditionId)
        {
            return await GetAllAsync(x => x.TournamentEditionId == tournamentEditionId && x.IsAccepted,
                includes: Includes, orderBy: OrderBy);
        }
    }
}
