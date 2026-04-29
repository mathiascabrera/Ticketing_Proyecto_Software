using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Seat?> GetByIdAsync(Guid id)
        {
            return await _context.Seats.FindAsync(id);
        }

        public async Task<List<Seat>> GetAllAsync()
        {
            return await _context.Seats.ToListAsync();
        }
        public async Task UpdateAsync(Seat seat)
        {
            try
            {
                _context.Seats.Update(seat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConcurrencyException();
            }
        }
        public async Task<List<Seat>> GetSeatsByEventIdAsync(int eventId)
        {
            return await _context.Seats
                .Include(s => s.SectorObj)
                .Where(s => s.SectorObj.EventId == eventId)
                .ToListAsync();
        }
    }
}
