using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;

namespace TennisTour.Application.Services
{
    public interface IRankingsService
    {
        Task<List<RankingsResponseModel>> GetAllRankings();
        Task UpdatePoints();
    }
}
