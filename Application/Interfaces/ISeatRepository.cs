using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat?> GetByIdAsync(Guid id);
        Task<List<Seat>> GetAllAsync();
        Task<List<Seat>> GetSeatsByEventIdAsync(int eventId);
        Task UpdateAsync(Seat seat);
    }
}
