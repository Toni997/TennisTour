using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Exceptions
{
    [Serializable]
    public class TennisRuleViolationException : Exception
    {
        public TennisRuleViolationException() { }
        public TennisRuleViolationException(string? message) : base(message) { }

        public TennisRuleViolationException(string? message, Exception? innerException) : base(message, innerException) { }

        protected TennisRuleViolationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
