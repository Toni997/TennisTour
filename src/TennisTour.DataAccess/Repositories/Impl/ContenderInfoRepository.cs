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
    public class ContenderInfoRepository : BaseRepository<ContenderInfo>, IContenderInfoRepository
    {
        public ContenderInfoRepository(DatabaseContext context) : base(context) { }

        private IIncludableQueryable<ContenderInfo, object> IncludesForGetOne(IQueryable<ContenderInfo> x)
        {
            return x.Include(x => x.Contender)
                        .ThenInclude(x => x.FavoritedByUsers)
                .Include(x => x.Contender)
                        .ThenInclude(x => x.Ranking);
        }

        public Task<ContenderInfo> GetContenderInfoOfUsenameAsync(string username)
        {
            // TODO we should change this to normalized username as it is indexed
            return GetOneOrNullAsync((contenderInfo) =>  contenderInfo.Contender.UserName == username);
        }

        public async Task<ContenderInfo> GetContenderInfoWithRankingByContenderIdAsync(string contenderId)
        {
            return await GetOneAsync(x => x.ContenderId == contenderId, IncludesForGetOne);
        }

        public async Task<bool> IsFavoritedByUser(string contenderId, string userId)
        {
            var contender = await GetOneAsync(x => x.ContenderId == contenderId, includes: x => x.Include(x => x.Contender).ThenInclude(x => x.FavoritedByUsers));
            var user = contender.Contender.FavoritedByUsers.FirstOrDefault(x => x.Id == userId);
            return user is not null;
        }
    }
}
