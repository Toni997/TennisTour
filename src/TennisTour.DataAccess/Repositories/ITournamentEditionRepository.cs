using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Repositories
{
    public interface ITournamentEditionRepository : IBaseRepository<TournamentEdition>
    {
        Task<TournamentEdition> GetByIdWithMatchesAsync(Guid id);

        Task<IList<TournamentEdition>> GetAllOrderedByDateStartDescAsync();
        Task<IList<TournamentEdition>> GetLastTenByContenderWithMatchesOrderedByDateStartDescAsync(string contenderId);
        Task<int> GetCareerTotalTitlesByContender(string contenderId);
    }
}
