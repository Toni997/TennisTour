using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;

namespace TennisTour.Core.Entities
{
    public class Ranking : BaseEntity
    {
        public ApplicationUser Contender { get; set; }
        public string ContenderId { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int? PreviousRank { get; set; }
        public int? PreviousPoints { get; set; }
        public int? BestRank { get; set; }
        public DateTime? BestRankDate { get; set; }
    }
}
