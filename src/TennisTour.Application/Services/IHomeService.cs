using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;

namespace TennisTour.Application.Services
{
    public interface IHomeService
    {
        Task<HomePageResponseModel> GetHomeAsync();
    }
}
