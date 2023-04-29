using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Entities
{
    public class Tournament : BaseAuditedEntity
    {
        public string Name { get; set; }
        public Series Series { get; set; }
        public Surface Surface { get; set; }
        public Series NumberOfRounds { get; set; }

        public virtual ICollection<TournamentEdition> TournamentEditions { get; set; }
        public virtual ICollection<TournamentRegistration> TournamentRegistrations { get; set; }
    }
}
