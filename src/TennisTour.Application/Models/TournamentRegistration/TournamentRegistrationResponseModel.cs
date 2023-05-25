using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.TournamentRegistration
{
    public class TournamentRegistrationForEditionResponseModel
    {
        public ContenderResponseModel Contender { get; set; }
        public bool IsAccepted { get; set; }
    }
}
