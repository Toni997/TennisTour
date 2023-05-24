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
        [Description("One-handed")]
        OneHanded = 0,

        [Description("Two-handed")]
        TwoHanded = 1
    }
}
