﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Repositories
{
    public interface ITournamentRepository : IBaseRepository<Tournament>
    {
        Task<Tournament> GetByIdWithTournamentEditionsAsync(Guid id);

        Task<List<Tournament>> GetAllOrderedByNameWithTournamentEditionsAsync();
    }
}
