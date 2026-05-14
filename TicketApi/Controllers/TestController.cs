using Microsoft.AspNetCore.Mvc;

namespace TicketApi.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("event")]
        public IActionResult GetEvent()
        {
            var evento = new
            {
                id = 1,
                name = "Concierto Demo",
                date = "2026-06-10",
                sectors = new[]
                {
                    new
                    {
                        id = "sector-1",
                        name = "VIP",
                        rows = 3,
                        cols = 5,
                        gridX = 10,
                        gridY = 10,
                        price = 5000
                    },
                    new
                    {
                        id = "sector-2",
                        name = "General",
                        rows = 5,
                        cols = 10,
                        gridX = 100,
                        gridY = 10,
                        price = 2000
                    }
                }
            };

            return Ok(evento);
        }
    }
}
