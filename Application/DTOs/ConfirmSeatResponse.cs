using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ConfirmSeatResponse
    {
        public Guid ReservationId { get; set; }
        public Guid SeatId { get; set; }
        public string Status { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}
