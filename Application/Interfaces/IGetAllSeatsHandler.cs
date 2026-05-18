using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.UsesCases.Seats.Queries;

namespace Application.Interfaces
{
    public interface IGetAllSeatsHandler
    {
        public Task<List<SeatResponse>> Handle(GetAllSeatsQuery query);
    }
}
