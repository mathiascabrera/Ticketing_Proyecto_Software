using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Seats.Queries;
using Domain.Entities;

namespace Application.UsesCases.Seats.Handlers
{
    public class GetAllSeatsHandler : IGetAllSeatsHandler
    {
        private readonly ISeatRepository _repository;

        public GetAllSeatsHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SeatResponse>> Handle(GetAllSeatsQuery query)
        {
            var seats = await _repository.GetAllAsync();

            return seats.Select(seat => new SeatResponse
            {
                Id = seat.Id,
                RowIdentifier = seat.RowIdentifier,
                SeatNumber = seat.SeatNumber
            }).ToList();
        }
    }
}
