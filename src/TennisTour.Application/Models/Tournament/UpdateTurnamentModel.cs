using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Application.Models.Tournament
{
    public class UpdateTurnamentModel : CreateTournamentModel
    {
        public Guid Id { get; set; }
    }
}
