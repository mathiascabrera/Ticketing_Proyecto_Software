using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class USER
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<RESERVATION> ReservationList { get; set; }
        public ICollection<AUDIT_LOG> AuditLogList { get; set; }
    }
}
