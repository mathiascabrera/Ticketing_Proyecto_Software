using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UsesCases.Reservations.Commands;

namespace Application.Interfaces
{
    public interface IReserveSeatHandler
    {
        public Task Handle(ReserveSeatCommand command);
    }
}
