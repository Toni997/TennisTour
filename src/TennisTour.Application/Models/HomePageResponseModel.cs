using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;
using TennisTour.Application.Models.TournamentEdition;

namespace TennisTour.Application.Models
{
    public class HomePageResponseModel
    {
        public List<RankingsResponseModel> TopTenRankedConenders { get; set; }
        public List<TournamentEditionResponseModel> LastTenFinishedTournaments { get; set; }
    }
}
