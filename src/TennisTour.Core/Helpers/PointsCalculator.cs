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
        private static readonly Dictionary<Series, List<int>> pointsMap = Enum.GetValues(typeof(Series)).Cast<Series>().Select(
            (e) =>
            {
                return e switch
                {
                    Series.TTChallenger => KeyValuePair.Create(e, new List<int> { 15, 32, 60, 100, 175 }),
                    Series.TT250 => KeyValuePair.Create(e, new List<int> { 20, 45, 90, 150, 250 }),
                    Series.TT500 => KeyValuePair.Create(e, new List<int> { 45, 90, 180, 300, 500 }),
                    Series.TTMasters1000 => KeyValuePair.Create(e, new List<int> { 10, 45, 90, 180, 360, 600, 1000 }),
                    Series.TTGrandSlam => KeyValuePair.Create(e, new List<int> { 10, 45, 90, 180, 360, 720, 1200, 2000 }),
                    Series.TTFinals => KeyValuePair.Create(e, new List<int> { 200, 200, 200, 400, 500 }),
                    _ => throw new NotImplementedException(),
                };
            }
            ).ToDictionary(pair => pair.Key, pair => pair.Value);

        public static int GetPoints(Series series, int round )
        {
            return pointsMap[series][round];
        }
    }
}
