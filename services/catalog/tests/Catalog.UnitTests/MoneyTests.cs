using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Catalog.UnitTests
{
    public class MoneyTests
    {
        [Fact]
        public void Two_Money_Objects_With_Same_Amount_And_Currency_Should_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(10, "TL");

            money1.Should().Be(money2);
            (money1 == money2).Should().BeTrue();
        }

        [Fact]
        public void Money_Objects_With_Different_Amounts_Should_Not_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(20, "TL");

            money1.Should().NotBe(money2);
            (money1 != money2).Should().BeTrue();
        }

        [Fact]
        public void Money_Objects_With_Different_Currencies_Should_Not_Be_Equal()
        {
            Money money1 = Money.Create(10, "TL");
            Money money2 = Money.Create(10, "USD");

            money1.Should().NotBe(money2);
            (money1 != money2).Should().BeTrue();
        }

        [Fact]
        public void Money_Create_With_Negative_Amount_Should_Throw_DomainException()
        {
            Action act = () => Money.Create(-5, "TL");

            act.Should().Throw<DomainException>();
        }


    }
}