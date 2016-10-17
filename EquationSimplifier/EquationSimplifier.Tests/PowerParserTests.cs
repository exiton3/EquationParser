
using System;
using System.Linq;
using EquationSimplification.Parser;
using NUnit.Framework;

namespace EquationSimplification.Tests
{
    [TestFixture]
    public class PowerParserTests
    {
        [Test]
        public void TestName()
        {
            string input = "x^2yz^3xa";

            var powerParser = new PowerParser();

            var result = powerParser.Parse(input).ToList()
                .OrderByDescending(x => x.Power)
                .ThenBy(x => x.Variable);

            foreach (var member in result)
            {
                Console.WriteLine("{0} - {1}", member.Variable, member.Power);
            }

         //   Assert.That(result[0].Power, Is.EqualTo(2));
          //  Assert.That(result[0].Variable, Is.EqualTo("x"));

        }
    }
}