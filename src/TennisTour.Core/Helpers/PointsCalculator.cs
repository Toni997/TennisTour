using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Helpers
{
    public class PointsCalculator
    {
        public static int GetPointsPerRoundForSeries(Series series)
        {
            return series switch
            {
                Series.TTChallenger => 30,
                Series.TT250 => 50,
                Series.TT500 => 100,
                Series.TTMasters1000 => 150,
                Series.TTGrandSlam => 200,
                Series.TTFinals => 200,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
