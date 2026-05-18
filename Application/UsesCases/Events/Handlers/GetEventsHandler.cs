using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Events.Queries;
using Domain.Entities;

namespace Application.UsesCases.Events.Handlers
{
    public class GetEventsHandler : IGetEventsHandler
    {
        private readonly IEventRepository _repository;

        public GetEventsHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EventResponse>> Handle(GetEventsQuery query)
        {
            var events = await _repository.GetAllAsync();

            return events.Select(e => new EventResponse
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Description = e.Description,
                Url1 = e.Url1,
                Url2 = e.Url2,
            }).ToList();
        }
    }
}
