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
    public class Reservation
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public User UserObj { get; set; }
        public ICollection<ReservationSeat> Seats { get; set; } = new List<ReservationSeat>();
    }
}
