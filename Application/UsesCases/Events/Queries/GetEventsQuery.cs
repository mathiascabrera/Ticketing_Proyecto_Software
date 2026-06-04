using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsesCases.Events.Queries
{
    public class GetEventsQuery
    {
        public int Page { get; set; } = 1; // Página solicitada
        public int PageSize { get; set; } = 10;     // Cantidad de elementos por página
    }
}
