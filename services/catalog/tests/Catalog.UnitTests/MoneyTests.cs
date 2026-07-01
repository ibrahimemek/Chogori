using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.UnitTests
{
    public class MoneyTests
    {
        [Fact]
        public void Two_Money_Objects_With_Same_Amount_And_Currency_Should_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(10, "TL");

            Assert.Equal(money1, money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Amounts_Should_Not_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(20, "TL");

            Assert.NotEqual(money1, money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Currencies_Should_Not_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(10, "USD");

            Assert.NotEqual(money1, money2);
        }

        [Fact]
        public void Money_Create_With_Negative_Amount_Should_Throw_DomainException()
        {
            Assert.Throws<DomainException>(() => Money.Create(-5, "TL"));
        }


    }
}
