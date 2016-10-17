using System;
using EquationSimplification.Lex;
using EquationSimplification.Parser;
using NUnit.Framework;

namespace EquationSimplification.Tests
{
    // 1. Support constants
    // 2. Right side
    // 3. Support brackets

    [TestFixture]
    public class EquationSimplifierTests
    {
        private Tokenizer tokenizer;
        private EquationSimplifier simplifier;

        [SetUp]
        public void SetUp()
        {
            tokenizer = new Tokenizer();
            simplifier = new EquationSimplifier(new EquationParser(tokenizer));
        }
        
        
        [Test]
        public void CanSimplifySimpleEquation()
        {
            var equationString = "-x^2  - y + 3x^2 + 2x^2y = y^2 - xy + y";

            var result = simplifier.Simplify(equationString);

           Assert.That(result, Is.EqualTo("2x^2 -2y + 2x^2y -y^2 + xy = 0"));
        }


        [Test]
        public void CanSimplifyEquationWithBrackets()
        {
            var equationString = "x - (3 - (y + 3x) + y) = y^2 - xy + y";

            var result = simplifier.Simplify(equationString);
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("4x -3 -y -y^2 + xy = 0"));
        }

        [Test]
        public void CanSimplifyIfRightPartIZero()
        {
            var equationString = "x  = 0";

            var result = simplifier.Simplify(equationString);
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("x = 0"));
        }

        [Test]
        public void CanSimplifyIfLeftPartBecomeZero()
        {
            var equationString = "x - x = 0";

            var result = simplifier.Simplify(equationString);
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("0 = 0"));
        }

        [Test]
        public void CanSimplifyIfLeftPartBecomeZeroFromConstants()
        {
            var equationString = "5 - 5 = 0";

            var result = simplifier.Simplify(equationString);
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("0 = 0"));
        }

        [Test]
        public void CanSimplifyIfLeftPartBecomeZeroFromConstants2()
        {
            var equationString = " 3xy -2yx = 0";

            var result = simplifier.Simplify(equationString);
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("xy = 0"));
        }

        [Test]
        public void Simplify_ThorwsException_IfWrongFormatSimpleEquation()
        {
            var equationString = "y";

            TestDelegate testDelegate = () => { simplifier.Simplify(equationString);};

            Assert.Throws<ArgumentException>(testDelegate);
        }
    }
}