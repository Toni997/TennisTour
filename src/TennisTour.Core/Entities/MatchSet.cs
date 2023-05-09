using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;

namespace TennisTour.Core.Entities
{
    public class MatchSet : BaseEntity
    {
        public int ContenderOneGamesCount { get; set; }
        public int ContenderTwoGamesCount { get; set; }
        public int LoserTiebreakPoints { get; set; }
        public int Order { get; set; }
        public virtual Match Match { get; set; }
        public Guid MatchId { get; set; }
    }
}
