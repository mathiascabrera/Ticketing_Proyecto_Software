using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTOs
{
    public class SeatResponse
    {
        public Guid Id { get; set; }
        public required string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
        public int Sector { get; set; }
    }
}
