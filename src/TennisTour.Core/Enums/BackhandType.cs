using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Enums
{
    public enum BackhandType
    {
        [Description("One-handed backhand")]
        OneHanded = 0,

        [Description("Two-handed backhand")]
        TwoHanded = 1
    }
}
