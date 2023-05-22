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

        public Task<ContenderInfo> GetContenderInfoOfUsenameAsync(string username)
        {
            return GetFirstAsync((contenderInfo) =>  contenderInfo.Contender.UserName == username);
        }
    }
}
