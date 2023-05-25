using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Core.Enums;

namespace TennisTour.Application.Models.Tournament
{
    public class TournamentResponseModel : BaseResponseModel
    {
        public string Name { get; set; }
        public Series Series { get; set; }
        public Surface Surface { get; set; }
        public int NumberOfRounds { get; set; }
    }

    public class TournamentWithEditionsResponseModel : TournamentResponseModel
    {
        public ICollection<TournamentEditionResponseModel> TournamentEditions { get; set; }
    }
}
