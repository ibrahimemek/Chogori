using Catalog.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; }
        public string CurrencyCode { get; }

        private Money(decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }
        public static Money Create(decimal amount, string currencyCode)
        {
            if (amount < 0)
                throw new DomainException("Price cannot be negative.");
            if (string.IsNullOrWhiteSpace(currencyCode)) 
                throw new DomainException("Currency code must be specified.");

            return new Money(amount, currencyCode);
        }
    }
}
