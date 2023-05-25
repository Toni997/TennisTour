using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Services
{
    public interface IContenderInfoService
    {
        Task<ContenderInfoModel> GetContenderInfoAsync(string contenderUsername);

        Task<ContenderInfoModel> EditContenderInfoAsync(ContenderInfoModel contenderInfo, ClaimsPrincipal claimsPrincipal);    
    }
}
