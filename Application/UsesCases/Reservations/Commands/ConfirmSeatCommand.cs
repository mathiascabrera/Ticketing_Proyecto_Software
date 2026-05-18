using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.UsesCases.Reservations.Commands
{
    public class ConfirmSeatCommand : IConfirmSeatCommand
    {
        public Guid ReservationId { get; set; }
    }
}
