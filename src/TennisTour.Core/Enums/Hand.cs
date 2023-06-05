using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Enums
{
    public enum Hand
    {
        [Description("Left-handed")]
        Left = 0,

        [Description("Right-handed")]
        Right = 1
    }
}
