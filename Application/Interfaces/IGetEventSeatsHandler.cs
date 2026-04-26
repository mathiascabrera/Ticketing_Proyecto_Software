using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Events.Queries;
using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGetEventSeatsHandler
    {

        public Task<List<SeatResponse>> Handle(GetEventSeatsQuery query);

    }
}
