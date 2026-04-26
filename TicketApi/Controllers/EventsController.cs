using Application.UseCases.Events.Queries;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Infrastructure.Persistence.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IGetEventSeatsHandler _handler;
        private readonly IGetEventsHandler _eventshandler;

        public EventsController(
            IGetEventSeatsHandler handler,
            IGetEventsHandler eventshandler)
        {
            _handler = handler;
            _eventshandler = eventshandler;
        }


        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _eventshandler.Handle(new GetEventsQuery());
            return Ok(result);
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // evento 
        [HttpGet("{id}/seats")]
        public async Task<IActionResult> GetSeats(int id)
        {
 
            var result = await _handler.Handle(new GetEventSeatsQuery(id));

            return Ok(result);
        }
        



    }
}
