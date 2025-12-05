using NUnit.Framework;
using ProjetoQA;

namespace ProjetoQA.Tests
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
            Assert.AreEqual(4, calc.Somar(2, 2));
        }

        [Test]
        public void TestSubtrair()
        {
            Assert.AreEqual(3, calc.Subtrair(5, 2));
        }
    }
}
