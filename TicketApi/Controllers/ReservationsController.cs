using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Application.UsesCases.Reservations.Handlers;
using Application.UsesCases.Reservations.Commands;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence.Repositories;
using Domain.Exeptions;
using Application.UseCases.Reservations.Commands;

namespace Api4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReserveSeatHandler _handler;
        private readonly IUserRepository _userRepository;
        private readonly IConfirmSeatHandler _confirmSeatHandler;
        public ReservationsController(
            IReserveSeatHandler handler,
            IUserRepository userRepository,
            IConfirmSeatHandler confirmSeatHandler)   /// borrar luego el UserRepository solo para prueba
        {
            _userRepository = userRepository; //// igual borrar solo para prueba
            _handler = handler;
            _confirmSeatHandler = confirmSeatHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Guid seatId)
        {
            var user = await _userRepository.GetByEmailAsync("test@test.com");

            if (user == null)                                           ///////
            {                                                           ///////    
                throw new Exception("User not found");                  ///////    BORRRAAAAAAAAAAAAAAAARRRRRR( luego..... ) !!!!
            }                                                           ///////

            try
            {
                var result = await _handler.Handle(new ReserveSeatCommand
                {
                    SeatId = seatId,
                    UserId = user.Id
                });

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message); //  409
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmReservation([FromBody] ConfirmSeatCommand command)
        {
            try
            {
                var result = await _confirmSeatHandler.Handle(command);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
