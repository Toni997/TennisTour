using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;

namespace TennisTour.Application.Services
{
    public interface IRankingsService
    {
        Task<List<RankingsModel>> GetAllRankings();
    }
}
