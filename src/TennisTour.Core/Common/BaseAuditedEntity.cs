using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Core.Common
{
    public abstract class BaseAuditedEntity : IAuditedEntity
    {
        public Guid Id { get; set; }
        public abstract string CreatedBy { get; set; }
        public abstract DateTime CreatedOn { get; set; }
        public abstract string UpdatedBy { get; set; }
        public abstract DateTime? UpdatedOn { get; set; }
    }
}
