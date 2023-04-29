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
    }
}
