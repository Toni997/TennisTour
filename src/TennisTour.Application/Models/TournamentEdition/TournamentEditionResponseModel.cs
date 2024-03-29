﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Match;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentRegistration;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.TournamentEdition
{
    public class TournamentEditionResponseModel : BaseResponseModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsRegistrationTimeOver { get; set; }
        public ContenderResponseModel Winner { get; set; }
        public TournamentResponseModel Tournament { get; set; }
    }

    public class TournamentEditionForScheduleResponseModel : TournamentEditionResponseModel
    {
        public bool IsAuthenticatedUserRegisteredToPlay { get; set; }
        public bool IsAuthenticatedUserAccepted { get; set; }
    }

    public class H2HMatchTournamentEditionResponseModel : BaseResponseModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsRegistrationTimeOver { get; set; }
        public TournamentResponseModel Tournament { get; set; }
    }

    public class TournamentEditionWithMatchesResponseModel : TournamentEditionResponseModel
    {
        public ICollection<MatchResponseModel> Matches { get; set; }
    }

    public class TournamentEditionWithMatchesForDetailsResponseModel : TournamentEditionWithMatchesResponseModel
    {
        public bool IsAuthenticatedUserRegisteredToPlay { get; set; }
        public int RegistrationsCount { get; set; }
    }
}
