using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum SeatStatus
    {
        Available = 0,
        Reserved = 1,
        Sold = 2
    }
    public class Seat
    {
        public Guid Id { get; set; }
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
        public int Version { get; set; }
        public virtual Sector SectorObj { get; set; }
        public Reservation? ReservationObj { get; set; }
    }
}
