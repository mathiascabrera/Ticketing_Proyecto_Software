using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.UsesCases.Reservations.Commands;

namespace Application.Interfaces
{
    public interface IReserveSeatHandler
    {
        public Task<ReserveSeatResponse> Handle(ReserveSeatCommand command);
    }
}
