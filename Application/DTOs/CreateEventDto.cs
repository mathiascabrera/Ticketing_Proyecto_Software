using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public string? Url1 { get; set; }
        public string? Url2 { get; set; }

        public List<CreateSectorDto> Sectors { get; set; } = new();
    }
}
