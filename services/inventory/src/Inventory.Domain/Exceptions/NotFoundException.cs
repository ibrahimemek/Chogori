using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class KetNotFoundException : NotFoundException
    {
        public KetNotFoundException(Guid productId)
            : base($"Stock item with product id '{productId}' was not found.") { }
    }
}
