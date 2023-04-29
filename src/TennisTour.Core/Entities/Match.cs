using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;

namespace TennisTour.Core.Entities
{
    public class Match : BaseAuditedEntity
    {
        public ApplicationUser ContenderOne { get; set; }
        public ApplicationUser ContenderTwo { get; set; }
        public ApplicationUser Winner { get; set; }
        [ForeignKey("NextMatch")]
        public Guid? NextMatchId { get; set; }
        public virtual Match NextMatch { get; set; }
        public TournamentEdition TournamentEdition { get; set; }
        public int Round { get; set; }

        public ICollection<MatchSet> MatchSets { get; set; }
    }
}
