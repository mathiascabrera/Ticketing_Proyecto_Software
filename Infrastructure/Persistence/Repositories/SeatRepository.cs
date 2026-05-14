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

        public async Task<Seat?> GetByIdAsync(Guid id)  // traer un asiento con ID
        {
            return await _context.Seats.FindAsync(id);
        }

        public async Task<List<Seat>> GetAllAsync() // traer todos los asientos 
        {
            return await _context.Seats.ToListAsync();
        }
        public async Task UpdateAsync(Seat seat) // actualizar 1 asiento  
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
        public async Task<List<Seat>> GetSeatsByEventIdAsync(int eventId) // traer todos los asientos de un evento
        {
            return await _context.Seats
                .Include(s => s.SectorObj)
                .Where(s => s.SectorObj.EventId == eventId)
                .ToListAsync();
        }

        public async Task<List<Seat>> GetByIdsAsync(List<Guid> seatsIds) // traer solo los asientos de la lista de guids
        {
            return await _context.Seats
                .Where(s => seatsIds.Contains(s.Id))
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Seat> seats)
        {
            await _context.Seats.AddRangeAsync(seats);
        }




    }
}
