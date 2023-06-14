using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Helpers
{
    public class RankChangeModel
    {
      

        public RankChangeModel(string contenderId, int round, Series series)
        {
            ContenderId = contenderId;
            Round = round;
            Series = series;
        }
        public string ContenderId;
        public int Round;
        public Series Series;
    }
}
