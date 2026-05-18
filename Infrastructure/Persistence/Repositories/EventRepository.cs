using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events
            .Include(e => e.SectorsList)
            .AsNoTracking()
            .ToListAsync();
        }

        // GET BY ID
        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.SectorsList)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // CREATE
        public async Task AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateAsync(Event ev)
        {
            _context.Events.Update(ev);
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);

            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
