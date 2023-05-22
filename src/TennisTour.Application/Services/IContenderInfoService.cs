using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Services
{
    public interface IContenderInfoService
    {
        Task<ContenderInfoResponseModel> GetContenderInfoAsync(string contenderUsername);

        Task<ContenderInfoResponseModel> EditContenderInfoAsync(ContenderInfoResponseModel contenderInfo);    
    }
}
