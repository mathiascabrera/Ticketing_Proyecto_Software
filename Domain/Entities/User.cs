using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {

        public ICollection<Reservation> ReservationList { get; set; } = new HashSet<Reservation>();
        public ICollection<AuditLog> AuditLogList { get; set; } = new HashSet<AuditLog>();
    }
}
