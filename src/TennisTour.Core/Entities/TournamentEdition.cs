using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;

namespace TennisTour.Core.Entities
{
    public class TournamentEdition : BaseAuditedEntity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsRegistrationTimeOver { get; set; }
        public ApplicationUser Winner { get; set; }
        public string? WinnerId { get; set; }
        public Tournament Tournament { get; set; }
        public Guid TournamentId { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<TournamentRegistration> TournamentRegistrations { get; set; }
    }
}
