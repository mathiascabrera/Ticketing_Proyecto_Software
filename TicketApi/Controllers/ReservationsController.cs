using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Application.UsesCases.Reservations.Handlers;
using Application.UsesCases.Reservations.Commands;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence.Repositories;
using Domain.Exeptions;

namespace Api4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReserveSeatHandler _handler;
        private readonly IUserRepository _userRepository;
        public ReservationsController(
            IReserveSeatHandler handler,
            IUserRepository userRepository)   /// borrar luego el UserRepository solo para prueba
        {
            _userRepository = userRepository; //// igual borrar solo para prueba
            _handler = handler;
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

                await _handler.Handle(new ReserveSeatCommand
                {
                    SeatId = seatId,
                    UserId = user.Id
                });

                return Ok("Reservation created");
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
    }
}
