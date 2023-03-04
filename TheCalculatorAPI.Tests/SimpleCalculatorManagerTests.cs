using System.Data;
using TheCalculator.Application.Interfaces;
using TheCalculator.Application.Models;

namespace TheCalculatorAPI.Tests
{
    public class Tests
    {
        private SimpleCalculatorManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new();
        }

        [Test]
        [TestCase("(1 + 2) * 4 / 6", 2.0)]
        [TestCase("3 * (4 + 5)", 27.0)]
        [TestCase("2 + 3 +(4 + 5) * 6", 59.0)]
        [TestCase("(5 - 9) / 2*3    -11", -17.0)]
        [TestCase("-2/-10", 0.2)]
        [TestCase("11/12", 0.92)]
        [TestCase("11 / 12", 0.92)]
        [TestCase("465465*465465.2", 216657759318.0)]
        [TestCase("465465*465465.234", 216657775143.81)]
        [TestCase("465465*465465", 216657666225.0)]
        [TestCase("465465 * 465465", 216657666225.0)]      
      
        public void Calculate_ReturnCorrectResult_WhenPasssingValidInput(string testExpression, decimal expectedResult)
        {
            var result = manager.Calcualte(GenerateCalculatorRequest(testExpression));
            Assert.That(result.Result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("test")]
        [TestCase("test data")]
        public void Calculate_ThrowsEvaluateException_WhenPasssingInvalidInput(string testExpression)
        {
            Assert.Throws<EvaluateException>(() => manager.Calcualte(GenerateCalculatorRequest(testExpression)));
        }

        [Test]
        [TestCase("9****9")]
        public void Calculate_ThrowsSyntaxErrorException_WhenPasssingInvalidInput(string testExpression)
        {
            Assert.Throws<SyntaxErrorException>(() => manager.Calcualte(GenerateCalculatorRequest(testExpression)));
        }

        [Test]
        [TestCase("")]
        public void Calculate_ThrowsArgumentNullException_WhenPasssingNullInput(string testExpression)
        {
            Assert.Throws<ArgumentNullException>(() => manager.Calcualte(null));
        }

        private CalculationRequest GenerateCalculatorRequest(string testExpression) => new CalculationRequest { Request = testExpression };
    }
}