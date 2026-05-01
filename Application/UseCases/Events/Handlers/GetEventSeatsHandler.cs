using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using Domain.Entities;

namespace Application.UseCases.Events.Handlers
{
    public class GetEventSeatsHandler : IGetEventSeatsHandler
    {
        private readonly ISeatRepository _seatRepository;

        public GetEventSeatsHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<List<SeatResponse>> Handle(GetEventSeatsQuery query)
        {
            var seats = await _seatRepository.GetSeatsByEventIdAsync(query.EventId);

            return seats.Select(s => new SeatResponse
            {
                Id = s.Id, //
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status,
                Sector = s.SectorId
            }).ToList();
        }
    }
}
