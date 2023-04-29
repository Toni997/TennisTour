﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;

namespace TennisTour.DataAccess.Repositories.Impl
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(DatabaseContext context) : base(context) { }
    }
}