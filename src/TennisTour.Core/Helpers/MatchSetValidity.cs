using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Helpers
{
    public struct MatchSetValidity
    {
        public bool IsValid { get; set; }
        public bool HasEnded { get; set; }

        public MatchSetValidity()
        {
            IsValid = false;
            HasEnded = false;
        }

        public MatchSetValidity(bool isValid, bool hasEnded)
        {
            IsValid = isValid;
            HasEnded = hasEnded;
        }
    }
}
