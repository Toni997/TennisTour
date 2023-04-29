using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;

namespace TennisTour.Core.Entities
{
    public class TournamentRegistration : BaseEntity
    {
        public ApplicationUser Contender { get; set; }
        public TournamentEdition TournamentEdition { get; set; }
        public bool IsAccepted { get; set; }
    }
}
