using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TennisTour.Core.Enums
{
    public enum Series
    {
        [Description("TT Challenger")]
        TTChallenger = 0,

        [Description("TT 250")]
        TT250 = 1,

        [Description("TT 500")]
        TT500 = 2,

        [Description("TT Masters 1000")]
        TTMasters1000 = 3,

        [Description("TT Grand Slam")]
        TTGrandSlam = 4,

        [Description("TT Finals")]
        TTFinals = 5
    }
}
