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

        public Task<IList<ContenderInfo>> GetContenderInfosByContenderIds(List<string> ids)
        {
            return GetAllAsync((contenderInfo) => ids.Contains(contenderInfo.Contender.Id));
        }

        public Task<ContenderInfo> GetContenderInfoByUsenameAsync(string username)
        {
            return GetOneOrNullAsync((contenderInfo) =>  contenderInfo.Contender.UserName == username);
        }
    }
}
