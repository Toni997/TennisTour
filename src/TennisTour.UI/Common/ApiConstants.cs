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
        public const string ContenderDetailsRoute = "ContenderInfo/{0}";
        public const string ContendersH2HRoute = "ContenderInfo/{0}/h2h/{1}";
        public const string FavoriteContender = "Users/Favorite/{0}";
        public const string UnfavoriteContender = "Users/Unfavorite/{0}";
        public const string RegisterToPlayTournament = "TournamentEditions/{0}/Register";
        public const string WithdrawTournamentRegistration = "TournamentEditions/{0}/Unregister";
        public const string GetAllRegistrationsForTournamentEdition = "TournamentEditions/{0}/Registrations";
        public const string GenerateRoundRoute = "TournamentEditions/{0}/GenerateRound";
        public const string ReportMatchResultRoute = "Matches/{0}/ReportResult";
        public const string ConfirmMatchResultRoute = "Matches/{0}/ConfirmResult";
        public const string MyFavoritesRoute = "Users/MyFavorites";
		public const string RankingsGetAll = "Rankings";
        public const string RankingsUpdate = "Rankings";
        public const string ConfirmUserRoute = "Users/confirmEmail";
        public const string ScheduleRoute = "TournamentEditions/Schedule";
    }
}

