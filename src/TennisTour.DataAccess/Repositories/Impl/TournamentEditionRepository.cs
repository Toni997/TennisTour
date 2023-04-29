﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;

namespace TennisTour.DataAccess.Repositories.Impl
{
    public class TournamentEditionRepository : BaseRepository<TournamentEdition>, ITournamentEditionRepository
    {
        public TournamentEditionRepository(DatabaseContext context) : base(context) { }
    }
}