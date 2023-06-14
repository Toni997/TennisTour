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
        private static readonly Dictionary<Series,int> pointsFactorsMap = Enum.GetValues(typeof(Series)).Cast<Series>().Select(
            (e) =>
            {
                return e switch
                {
                    Series.TTChallenger => KeyValuePair.Create(e, 30),
                    Series.TT250 => KeyValuePair.Create(e, 50),
                    Series.TT500 => KeyValuePair.Create(e, 100),
                    Series.TTMasters1000 => KeyValuePair.Create(e, 150),
                    Series.TTGrandSlam => KeyValuePair.Create(e, 200),
                    Series.TTFinals => KeyValuePair.Create(e, 200),
                    _ => throw new NotImplementedException(),
                };
            }
            ).ToDictionary(pair => pair.Key, pair => pair.Value);

        public static int GetPoints(Series series, int round)
        {
            return pointsFactorsMap[series] * round;
        }
    }
}
