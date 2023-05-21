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
        public const string TournamentsGetOneRoute = "Tournaments/%s";
        public const string LoginRoute = "Users/authenticate";
        public const string RegisterRoute = "Users";
    }
}
