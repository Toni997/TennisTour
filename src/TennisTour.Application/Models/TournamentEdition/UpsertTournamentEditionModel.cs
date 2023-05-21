using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.TournamentEdition
{
    public class UpsertTournamentEditionModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid TournamentId { get; set; }
    }

    public class UpsertTournamentEditionResponseModel : BaseResponseModel { }
}
