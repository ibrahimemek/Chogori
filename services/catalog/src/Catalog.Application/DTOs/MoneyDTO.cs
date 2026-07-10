using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.DTOs
{
    public record MoneyDTO(
        decimal Amount,
        string CurrenyCode);
    
}
