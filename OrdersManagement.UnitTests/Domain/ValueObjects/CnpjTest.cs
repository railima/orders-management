using System;
using Xunit;
using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.UnitTests.Domain.ValueObjects
{
    public class CnpjTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("11111111111111")]
        public void Constructor_InvalidCnpj_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => new Cnpj(input));
        }

        [Fact]
        public void Constructor_ValidCnpj_SetsValue()
        {
            var validCnpj = "04.470.781/0001-39";
            var cnpj = new Cnpj(validCnpj);
            Assert.Equal(validCnpj, cnpj.Value);
        }
    }
}