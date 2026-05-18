using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.UsesCases.Reservations.Commands;

namespace Application.Interfaces
{
    public interface IConfirmSeatHandler
    {
        public Task<ConfirmSeatResponse> Handle(ConfirmSeatCommand command);
    }
}
