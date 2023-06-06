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
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<Match, object> IncludesForH2H(IQueryable<Match> x)
        {
            return x.Include(x => x.Winner)
                        .ThenInclude(x => x.ContenderInfo)
                    .Include(x => x.TournamentEdition)
                        .ThenInclude(x => x.Tournament);
        }

        public async Task<int> GetCareerTotalWinsByContender(string contenderId)
        {
            var careerWins = await GetAllAsync(x => x.WinnerId == contenderId);
            return careerWins.Count;
        }

        public async Task<int> GetCareerTotalLosesByContender(string contenderId)
        {
            var careerLoses = await GetAllAsync(x => (x.ContenderOneId == contenderId || x.ContenderTwoId == contenderId) &&
                                                        x.WinnerId != contenderId);
            return careerLoses.Count;
        }

        public async Task<int> GetCareerTotalH2HWinsByContenderOneAgainstContenderTwo(string contenderOneId, string contenderTwoId)
        {
            var h2hWins = await GetAllAsync(x => (x.ContenderOneId == contenderOneId && x.ContenderTwoId == contenderTwoId ||
                                                        x.ContenderOneId == contenderTwoId && x.ContenderTwoId == contenderOneId) &&
                                                        x.WinnerId == contenderOneId);
            return h2hWins.Count;
        }

        public async Task<IList<Match>> GetAllH2HMatchesBetweenContenderOneAndContenderTwo(string contenderOneId, string contenderTwoId)
        {
            return await GetAllAsync(x => x.ContenderOneId == contenderOneId && x.ContenderTwoId == contenderTwoId ||
                                            x.ContenderOneId == contenderTwoId && x.ContenderTwoId == contenderOneId,
                                    includes: IncludesForH2H);
        }
    }
}
