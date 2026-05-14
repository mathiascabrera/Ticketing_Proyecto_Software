using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateSectorDto
    {
        public long Id { get; set; } // id del front (timestamp)
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public decimal Price { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
    }
}
