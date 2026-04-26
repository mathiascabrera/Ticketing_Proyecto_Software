using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.UsesCases.Reservations.Commands;
using Domain.Entities;
using Domain.Exeptions;

namespace Application.UsesCases.Reservations.Handlers
{
    public class ReserveSeatHandler : IReserveSeatHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRepository _reservationRepository;


        public ReserveSeatHandler(
            ISeatRepository seatRepository,
            IReservationRepository reservationRepository)
        {
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(ReserveSeatCommand command)
        {
            var seat = await _seatRepository.GetByIdAsync(command.SeatId);// trae el asiento 

            if (seat == null)
                throw new Exception("Seat not found");

            if (seat.Status != SeatStatus.Available)
                throw new BusinessException("Seat is not available");

            var hasReservation = await _reservationRepository.HasActiveReservation(command.SeatId);// verifica si esta reservado

            if (hasReservation)
                throw new Exception("Seat already reserved");

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SeatId = command.SeatId,
                UserId = command.UserId,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                Status = ReservationStatus.Pending
            };

            seat.Status = SeatStatus.Reserved;
            seat.Version++;

            await _seatRepository.UpdateAsync(seat);
            await _reservationRepository.AddAsync(reservation);
        }
    }
}
