using NUnit.Framework;
using AutomacaoTestes;

namespace AutomacaoTestes.Tests
{
    [TestFixture]
    public class CalculadoraTests
    {
        private Calculadora calc;

        [SetUp]
        public void Setup()
        {
            calc = new Calculadora();
        }

        [Test]
        public void TestSomar()
        {
            Assert.AreEqual(5, calc.Somar(2, 3));
        }

        [Test]
        public void TestSubtrair()
        {
            Assert.AreEqual(1, calc.Subtrair(3, 2));
        }
    }
}
