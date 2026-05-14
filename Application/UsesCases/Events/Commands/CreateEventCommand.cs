using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.UsesCases.Events.Commands
{
    public class CreateEventCommand
    {
        public CreateEventDto dataEvent { get; set; }

        public CreateEventCommand(CreateEventDto dto)
        {
            dataEvent = dto;
        }
    }
}
