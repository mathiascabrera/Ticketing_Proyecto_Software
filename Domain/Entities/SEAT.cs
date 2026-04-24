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
    public class SEAT
    {
        public Guid Id { get; set; }
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
        public int Version { get; set; }
        public SECTOR SectorObj { get; set; }
        public RESERVATION? ReservationObj { get; set; }
    }
}
