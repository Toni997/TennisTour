using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.TournamentEdition
{
    public class TournamentEditionResponseModel : BaseResponseModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsRegistrationTimeOver { get; set; }
        public ApplicationUser Winner { get; set; }
        public string? WinnerId { get; set; }
    }
}
