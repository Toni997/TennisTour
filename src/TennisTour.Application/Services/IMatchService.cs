using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models;
using TennisTour.Core.Models;

namespace TennisTour.Application.Services
{
    public interface IMatchService
    {
        Task<BaseResponseModel> ReportResult(Guid id, UpsertMatchSetsModel upsertMatchSetsModel, string authenticatedContenderId);
        Task<BaseResponseModel> ConfirmResult(Guid id, string authenticatedContenderId);
    }
}
