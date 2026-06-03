using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Events.Queries;
using Domain.Entities;
using Domain.Exeptions;

namespace Application.UsesCases.Events.Handlers
{
    public class GetEventsHandler : IGetEventsHandler
    {
        private readonly IEventRepository _repository;

        public GetEventsHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<EventResponse>> Handle(GetEventsQuery query)
        {
            if (query.Page <= 0)
                throw new ValidationException("Page must be greater than 0.");

            if (query.PageSize <= 0)
                throw new ValidationException("PageSize must be greater than 0.");

            var events = await _repository.GetPagedAsync(
                query.Page,
                query.PageSize);

            var totalItems = await _repository.CountAsync();

            var eventResponses = events.Select(e => new EventResponse
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Description = e.Description,
                Url1 = e.Url1,
                Url2 = e.Url2
            }).ToList();

            return new PagedResponse<EventResponse>
            {
                Items = eventResponses,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)query.PageSize)
            };
        }
    }
}
