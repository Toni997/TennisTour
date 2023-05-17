using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;

namespace TennisTour.Application.Models.Tournament
{
    public class CreateTournamentModel
    {
        public string Name { get; set; }
        public Series Series { get; set; }
        public Surface Surface { get; set; }
        public int NumberOfRounds { get; set; }
    }

    public class CreateTournamentResponseModel : BaseResponseModel { }
}
