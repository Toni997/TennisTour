using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Application.Models.Match
{
    public class ContenderMatchupModel : BaseResponseModel
    {
        public string FullName { get; set; }
        public IEnumerable<MarkupString> GamesPerSet { get; set; }
    }
}
