using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.UsesCases.Reservations.Commands;

namespace Application.Interfaces
{
    public interface IReserveSeatsHandler
    {
        public Task<ReserveSeatsResponse> Handle(ReserveSeatsCommand command, string userId);
    }
}
