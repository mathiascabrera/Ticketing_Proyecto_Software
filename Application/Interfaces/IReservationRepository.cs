using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservationRepository
    {
        public void Update(Reservation reservation);
        public Task<Reservation?> GetByIdAsync(Guid id);
        public Task AddAsync(Reservation reservation);
        public Task<Reservation> GetByIdWithSeats(Guid id);
    }
}
