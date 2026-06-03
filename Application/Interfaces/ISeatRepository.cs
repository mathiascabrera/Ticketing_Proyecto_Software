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
        void Update(Seat seat);
        Task<List<Seat>> GetByIdsAsync(List<Guid> seatsIds);
        Task AddRangeAsync(IEnumerable<Seat> seats);
    }
}
