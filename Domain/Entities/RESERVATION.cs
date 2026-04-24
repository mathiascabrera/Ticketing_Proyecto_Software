using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum ReservationStatus
    {
        Pending = 0,
        Paid = 1,
        Expired = 2
    }
    public class RESERVATION
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public USER UserObj { get; set; }
        public SEAT SeatObj { get; set; }
    }
}
