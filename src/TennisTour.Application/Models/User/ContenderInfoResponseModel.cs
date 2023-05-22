using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.Core.Enums;

namespace TennisTour.Application.Models.User
{
    public class ContenderInfoResponseModel: BaseResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int WeightKg { get; set; }
        public int HeightCm { get; set; }
        public BackhandType BackhandType { get; set; }
        public Hand DominantHand { get; set; }
        public DateTime? TurnedProOn { get; set; }
        public DateTime? RetiredOn { get; set; }
    }
}
