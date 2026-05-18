using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UsesCases.Events.Queries;
using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGetEventSeatsHandler
    {

        Task<List<SectorSeatsResponse>> Handle(GetEventSeatsQuery query);

    }
}
