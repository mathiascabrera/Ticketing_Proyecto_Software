using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
            : base("The record was modified by another user") { }
    }
}
