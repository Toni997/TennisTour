using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.UI.Common
{
    public static class ApiConstants
    {
        public const string BaseUrl = "https://localhost:5001/api/";
        public const string TournamentsGetAllRoute = "Tournaments";
        public const string TournamentsSearchAllRoute = "Tournaments/Search?value={0}";
        public const string TournamentsGetOneRoute = "Tournaments/{0}";
        public const string TournamentsAddRoute = "Tournaments";
        public const string TournamentsEditRoute = "Tournaments/{0}";
        public const string TournamentsDeleteRoute = "Tournaments/{0}";
        public const string TournamentEditionsGetAllRoute = "TournamentEditions";
        public const string TournamentEditionsGetOneRoute = "TournamentEditions/{0}";
        public const string TournamentEditionsAddRoute = "TournamentEditions";
        public const string TournamentEditionsEditRoute = "TournamentEditions/{0}";
        public const string TournamentEditionsDeleteRoute = "TournamentEditions/{0}";
        public const string LoginRoute = "Users/authenticate";
        public const string RegisterRoute = "Users";
        public const string SaveContenderInfo = "ContenderInfo";
        public const string GetContenderInfo = "ContenderInfo?contenderUsername={0}";
    }
}
