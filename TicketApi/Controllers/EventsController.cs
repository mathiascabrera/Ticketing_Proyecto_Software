using Application.UsesCases.Events.Queries;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Application.DTOs;
using Application.UsesCases.Events.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketApi.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IGetEventSeatsHandler _handler;
        private readonly IGetEventsHandler _eventshandler;
        private readonly ICreateEventHandler _createEventHandler;

        public EventsController(
            IGetEventSeatsHandler handler,
            IGetEventsHandler eventshandler,
            ICreateEventHandler createEventHandler ) 
        {
            _handler = handler;
            _eventshandler = eventshandler;
            _createEventHandler = createEventHandler;
        }

        // -------------------------
        // GET ALL EVENT
        // -------------------------
        [HttpGet("events")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _eventshandler.Handle(new GetEventsQuery());
            return Ok(result);
        }

        // -------------------------
        // GET EVENT BY ID      (NO IMPLEMENTADO)
        // -------------------------
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // -------------------------
        // CREATE EVENT         (NO IMPLEMENTADO)
        // -------------------------
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            var command = new CreateEventCommand(dto);

            var id = await _createEventHandler.Handle(command);

            return StatusCode(201, new { id });
        }

        // -------------------------
        // ACTUALIZAR EVENT BY ID   (NO IMPLEMENTADO)
        // -------------------------
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // -------------------------
        // DELETE EVENT     (NO IMPLEMENTADO)
        // -------------------------
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // -------------------------
        // GET SEATS FROM EVENT BY ID 
        // -------------------------
        [HttpGet("{id}/seats")]
        public async Task<IActionResult> GetSeats(int id)
        {
 
            var result = await _handler.Handle(new GetEventSeatsQuery(id));

            return Ok(result);
        }
        



    }
}
