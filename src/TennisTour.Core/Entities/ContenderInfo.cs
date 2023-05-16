using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Common;
using TennisTour.Core.Enums;

namespace TennisTour.Core.Entities
{
    public class ContenderInfo : BaseEntity
    {
        public ApplicationUser Contender { get; set; }
        public string ContenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int WeightKg { get; set; }
        public int HeightCm { get; set; }
        public BackhandType BackhandType { get; set; }
        public Hand DominantHand { get; set; }
        public DateTime TurnedProOn { get; set; } = DateTime.Now;
        public DateTime? RetiredOn { get; set; }
    }
}
