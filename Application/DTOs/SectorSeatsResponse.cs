using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SectorSeatsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<SeatResponse> Seats { get; set; }
    }
}
