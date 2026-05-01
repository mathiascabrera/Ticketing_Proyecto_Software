using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Events.Queries;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IGetEventsHandler
    {
        public Task<List<EventResponse>> Handle(GetEventsQuery query);


    }
}
