using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UsesCases.Events.Queries;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IGetEventsHandler
    {
        Task<PagedResponse<EventResponse>> Handle(GetEventsQuery query);


    }
}
