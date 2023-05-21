using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Enums
{
    public enum Surface
    {
        [Description("Grass")]
        Grass = 0,

        [Description("Clay")]
        Clay = 1,

        [Description("Hard")]
        Hard = 2
    }
}
