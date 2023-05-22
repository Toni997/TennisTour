using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.MatchSet;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.Models.Match
{
    public class MatchResponseModel : BaseResponseModel
    {
        public ContenderResponseModel ContenderOne { get; set; }
        public ContenderResponseModel ContenderTwo { get; set; }
        public ContenderResponseModel Winner { get; set; }
        public int NextMatchupControlNumber { get; set; }
        public int Round { get; set; }

        public ICollection<MatchSetResponseModel> MatchSets { get; set; }
    }
}
