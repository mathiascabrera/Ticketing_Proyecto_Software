using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message) { }

        public ConcurrencyException(string message, Exception inner)
            : base(message, inner) { }

        public ConcurrencyException()
            : base("Error de concurrencia") { }

    }
}
