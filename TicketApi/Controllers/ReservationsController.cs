using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Application.UsesCases.Reservations.Handlers;
using Application.UsesCases.Reservations.Commands;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence.Repositories;
using Domain.Exeptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Api4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReserveSeatsHandler _handler;

        private readonly IConfirmSeatHandler _confirmSeatHandler;
        public ReservationsController(
            IReserveSeatsHandler handler,
            IConfirmSeatHandler confirmSeatHandler)  
        {
            _handler = handler;
            _confirmSeatHandler = confirmSeatHandler;
        }


        // -------------------------
        // CREATE RESERVATION                       // CONSULTAR SI... TOMAMOS EL TOKEN ACA PARA EL ID DEL USUARIO O LE PASAMOS EL TOKEN AL HANDLER 
        // -------------------------                // Y QUE SAQUE EL ID DEL USUARIO DENTRO DEL HANDLER ... AL IGUAL QUE EL LOGIN Y EL REGISTER
        [HttpPost]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateReservation([FromBody] ReserveSeatsCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var result = await _handler.Handle(command, userId);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // -------------------------
        // CONFIRM RESERVATION   
        // -------------------------
        [HttpPost("confirm")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ConfirmReservation([FromBody] ConfirmSeatCommand command)
        {
            try
            {
                var result = await _confirmSeatHandler.Handle(command);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
