
using EquationSimplification.Parser;
using NUnit.Framework;

namespace EquationSimplification.Tests
{
    [TestFixture]
    public class EquationMemberTests
    {
        [Test]
        public void Normalize_OrderVaribalesByPowerAndSymbol()
        {
            var member = new EquationMember("x^2yz^3da", 1);

            member.Normalize();

            Assert.That(member.Parameter,Is.EqualTo("z^3x^2ady"));
        }

        [Test]
        public void Normalize_DoesNotChangeParamiterIfIsConst()
        {
            var member = new EquationMember("Const", 1);

            member.Normalize();

            Assert.That(member.Parameter, Is.EqualTo("Const"));
        }
    }
}