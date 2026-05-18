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
    public class GetEventSeatsHandler : IGetEventSeatsHandler
    {
        private readonly ISeatRepository _seatRepository;

        public GetEventSeatsHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<List<SectorSeatsResponse>> Handle(GetEventSeatsQuery query)
        {
            var seats = await _seatRepository.GetSeatsByEventIdAsync(query.EventId);

            return seats
                .GroupBy(s => new
                {
                    s.SectorObj.Id,
                    s.SectorObj.Name,
                    s.SectorObj.Price,
                    s.SectorObj.Rows,
                    s.SectorObj.Cols,
                    s.SectorObj.GridX,
                    s.SectorObj.GridY,
                })
                .Select(g => new SectorSeatsResponse
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Price = g.Key.Price,
                    X = g.Key.GridX,
                    Y = g.Key.GridY,
                    Rows = g.Key.Rows,
                    Cols = g.Key.Cols,


                    Seats = g.Select(s => new SeatResponse
                    {
                        Id = s.Id,
                        RowIdentifier = s.RowIdentifier,
                        SeatNumber = s.SeatNumber,
                        Status = s.Status
                    }).ToList()
                })
                .ToList();
        }
    }
}
