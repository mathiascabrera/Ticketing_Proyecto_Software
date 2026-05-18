using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsesCases.Seats.Queries
{
    public class GetSeatByIdQuery
    {
        public Guid Id { get; set; }

        public GetSeatByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
