using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDbContext _context;

        public SectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Sector> sectors)
        {
            await _context.Sectors.AddRangeAsync(sectors);
        }
    }
}
