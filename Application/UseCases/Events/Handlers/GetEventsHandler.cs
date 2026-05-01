using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using Domain.Entities;
using static System.Net.WebRequestMethods;

namespace Application.UseCases.Events.Handlers
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
                EventDate = e.EventDate, // no dimos con el tiempo y en ves de cargar la url en el evento y levantarla de la base la pusimos aca
                ImangenUrl = "https://media.ambito.com/p/7779360f0b5ae7022756553912b7b626/adjuntos/239/imagenes/038/942/0038942625/la-rengajpg.jpg",
                Sectors = e.SectorsList?.Select(s => new SectorResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price
                }).ToList() ?? new List<SectorResponse>()
            }).ToList();
        }
    }
}
