using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.User
{
    public class ContenderResponseModel : BaseResponseModel
    {
        public RankingResponseModel Ranking { get; set; }
        public ContenderInfoModel ContenderInfo { get; set; }

        public override string? ToString()
        {
            return $"{ContenderInfo.FirstName} {ContenderInfo.LastName}";
        }
    }
}
