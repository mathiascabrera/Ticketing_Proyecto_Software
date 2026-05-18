using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.UsesCases.Reservations.Commands
{
    public class ReserveSeatsCommand :  IReserveSeatsCommand
    {
        public List<Guid> SeatsIds { get; set; }
    }
}
