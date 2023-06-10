using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Repositories
{
    public interface ITournamentRegistrationRepository : IBaseRepository<TournamentRegistration>
    {
        Task<bool> IsContenderRegisteredForTournamentEdition(string contenderId, Guid tournamentEditionId);
        Task<IList<TournamentRegistration>> GetAllByTournamentEditionAsync(Guid tournamentEditionId);
        Task<IList<TournamentRegistration>> GetAllAcceptedByTournamentEditionAsync(Guid tournamentEditionId);
    }
}
