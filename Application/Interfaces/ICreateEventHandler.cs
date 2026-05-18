using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UsesCases.Events.Commands;

namespace Application.Interfaces
{
    public interface ICreateEventHandler
    {
        Task<int> Handle(CreateEventCommand command);
    }
}
