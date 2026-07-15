using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Exceptions
{
    public class InsufficentStockException : Exception
    {
        public InsufficentStockException(string message): base(message) { }
    }
}
