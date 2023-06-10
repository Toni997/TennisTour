using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Repositories
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<int> GetCareerTotalWinsByContender(string contenderId);
        Task<int> GetCareerTotalLosesByContender(string contenderId);
        Task<int> GetCareerTotalH2HWinsByContenderOneAgainstContenderTwo(string contenderOneId, string contenderTwoId);
        Task<IList<Match>> GetAllH2HMatchesBetweenContenderOneAndContenderTwo(string contenderOneId, string contenderTwoId);
    }
}
