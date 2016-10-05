using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

using static System.Console;


namespace EquationSimplifier.Tests
{
    [TestFixture]
    public class TokenizerTests
    {

        [Test]
        public void CanParse()
        {
            string equationString = "x^2 + 3.5xy + y = y^2 - xy + y";

            var replace = equationString.Replace(" ", "");
            var strings = replace.Split('=');
            string leftPart = strings[0];
            string rightPart = strings[1];

            var s1 = leftPart.Split(new[] {"+", "-"}, StringSplitOptions.RemoveEmptyEntries);


            WriteLine(rightPart);
        }
    }
}
