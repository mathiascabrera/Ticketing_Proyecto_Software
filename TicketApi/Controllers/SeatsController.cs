using Application.Interfaces;
using Application.UsesCases.Seats.Handlers;
using Application.UsesCases.Seats.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api4.Controllers          ///////////Query/Command = datos (no comportamiento)
                                    ///////////Interfaces se usan para abstraer comportamiento osea handler
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IGetSeatByIdHandler _handler;     //inyeccion del handler .1
        private readonly IGetAllSeatsHandler _getAllHandler;  //inyeccion de getallhandler

        public SeatsController( 
            IGetSeatByIdHandler handler,// inyeccion automatica de ambas clases
            IGetAllSeatsHandler getAllHandler) 
         {
            _handler = handler;
            _getAllHandler = getAllHandler;
         }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) //recive el id  del asiento clickeado por front
        {
            var query = new GetSeatByIdQuery(id); // se crea la query con el id que se pide .3
            var result = await _handler.Handle(query); // el hadler toma la query y devuelve lo que pide la query  4.

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // en este caso no resive nada , solo devuelve los datos de todos lo asientos para construir matris de asientos
        {
            var result = await _getAllHandler.Handle(new GetAllSeatsQuery()); // crea la query y se lo pasa al getallhandler directamente no como arriba en 2 renglones
            return Ok(result);
        }

    }
}
