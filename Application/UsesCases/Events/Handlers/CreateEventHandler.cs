using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.UsesCases.Events.Commands;
using Domain.Entities;

namespace Application.UsesCases.Events.Handlers
{
    public class CreateEventHandler : ICreateEventHandler
    {
        private readonly IEventRepository _eventRepo;
        private readonly ISectorRepository _sectorRepo;
        private readonly ISeatRepository _seatRepo;

        public CreateEventHandler(
            IEventRepository eventRepo,
            ISectorRepository sectorRepo,
            ISeatRepository seatRepo)
        {
            _eventRepo = eventRepo;
            _sectorRepo = sectorRepo;
            _seatRepo = seatRepo;
        }

        public async Task<int> Handle(CreateEventCommand command)
        {
            var dto = command.dataEvent;

            var evento = new Event
            {
                Name = dto.Name,
                EventDate = dto.Date,
                Venue = dto.Place,
                Description = dto.Description,
                Status = dto.State,
                Url1 = dto.Url1,
                Url2 = dto.Url2
            };

            await _eventRepo.AddAsync(evento);

            var sectors = new List<Sector>();
            var seats = new List<Seat>();

            foreach (var s in dto.Sectors)
            {
                var sector = new Sector
                {
                    EventObj = evento,
                    Name = s.Name,
                    Price = s.Price,
                    Rows = s.Rows,
                    Cols = s.Cols,
                    GridX = s.GridX,
                    GridY = s.GridY,
                    Capacity = s.Rows * s.Cols
                };

                sectors.Add(sector);

                // generar seats automáticamente
                for (int r = 0; r < s.Rows; r++)
                {
                    for (int c = 0; c < s.Cols; c++)
                    {
                        seats.Add(new Seat
                        {
                            SectorObj = sector,
                            RowIdentifier = ((char)('A' + r)).ToString(),
                            SeatNumber = c + 1,
                            Status = SeatStatus.Available
                        });
                    }
                }
            }

            await _sectorRepo.AddRangeAsync(sectors);
            await _seatRepo.AddRangeAsync(seats);
            await _eventRepo.SaveAsync();

            return evento.Id;
        }
    }
}
