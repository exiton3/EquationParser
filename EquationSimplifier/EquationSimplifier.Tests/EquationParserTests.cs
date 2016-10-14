using System;
using System.Collections.Generic;
using System.Linq;
using EquationSimplification.Lex;
using EquationSimplification.Parser;
using NUnit.Framework;

namespace EquationSimplification.Tests
{
    [TestFixture]
    public class EquationParserTests
    {
        private Tokenizer tokenizer;
        private EquationParser parser;

        [SetUp]
        public void SetUp()
        {
            tokenizer = new Tokenizer();
            parser = new EquationParser(tokenizer);

        }

        [Test]
        public void CanParseBrackets()
        {
            string input = "x - (4 - 4x(3 - x))";

            var result = parser.Parse(input);

            Print(result);

        }

       

        private static void Print(IEnumerable<EquationMember> result)
        {
            foreach (var equationMember in result)
            {
                Console.WriteLine("{0} - {1}", equationMember.Parameter, equationMember.Coef);
            }
        }

        [Test]
        public void CanParseConstants()
        {
            var equationString = "3x + 5";

            var result = parser.Parse(equationString).ToList();

            Print(result);
            Assert.That(result[1].Parameter, Is.EqualTo("Const"));
            Assert.That(result[1].Coef, Is.EqualTo(5.0));
        }


        [Test]
        public void CanParseNegativeConstants()
        {
            var equationString = "3x - 5";

            var result = parser.Parse(equationString).ToList();

            Print(result);
            Assert.That(result[1].Parameter, Is.EqualTo("Const"));
            Assert.That(result[1].Coef, Is.EqualTo(-5.0));
        }


        [Test]
        public void CanParseDoubleNegativeConstants()
        {
            var equationString = "3x - -5";

            var result = parser.Parse(equationString).ToList();

            Print(result);
            Assert.That(result[1].Parameter, Is.EqualTo("Const"));
            Assert.That(result[1].Coef, Is.EqualTo(5.0));
        }


    }
}