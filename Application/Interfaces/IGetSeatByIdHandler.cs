using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Seats.Queries;

namespace Application.Interfaces
{
    public interface IGetSeatByIdHandler
    {
        public Task<SeatResponse> Handle(GetSeatByIdQuery query);

    }
}
