using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Seats.Queries;

namespace Application.UsesCases.Seats.Handlers
{
    public class GetSeatByIdHandler : IGetSeatByIdHandler
    {
        private readonly ISeatRepository _repository;   /// inyeccion del Iseat "alguien me va a dar los datos" .1

        public GetSeatByIdHandler(ISeatRepository repository)  ///// inyecta el repo automaticamente  .2
        {
            _repository = repository;
        }

        public async Task<SeatResponse> Handle(GetSeatByIdQuery query)   /// recive el query con el pedido query 3.
        {
            var seat = await _repository.GetByIdAsync(query.Id);// crea un asiento con el id de la query

            if (seat == null) // validacion simple que el id del asiento no sea null... hay que agregar mas logica para prevenir errores
                return null;

            return new SeatResponse  //  crea el dto y lo devuelve 
            {
                Id = seat.Id,
                RowIdentifier = seat.RowIdentifier,
                SeatNumber = seat.SeatNumber,
       
            };
        }
    }
}
