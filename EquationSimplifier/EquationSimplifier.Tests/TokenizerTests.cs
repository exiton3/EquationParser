using System;
using System.Collections.Generic;
using System.Linq;
using EquationSimplification.Lex;
using NUnit.Framework;

namespace EquationSimplification.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        private Tokenizer tokenizer;

        [Test]
        public void Tokenize_ReturnsTokensForVariableEquationMembers()
        {
            string input = "x^2 + y";

            tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(input).ToList();

            Print(tokens);


            Assert.That(tokens.Count,Is.EqualTo(3));
            Assert.That(tokens[0].Name, Is.EqualTo(TokenKind.Power));
            Assert.That(tokens[0].Value, Is.EqualTo("x^2"));
            Assert.That(tokens[1].Name, Is.EqualTo(TokenKind.Plus));
            Assert.That(tokens[2].Name, Is.EqualTo(TokenKind.Power));
        }

        [Test]
        public void Tokenize_ReturnsTokensForConstantsEquationMembers()
        {
            string input = "2 + y";

            tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(input).ToList();

            Print(tokens);


            Assert.That(tokens.Count, Is.EqualTo(3));
            Assert.That(tokens[0].Name, Is.EqualTo(TokenKind.Constant));
            Assert.That(tokens[0].Value, Is.EqualTo("2"));
            Assert.That(tokens[1].Name, Is.EqualTo(TokenKind.Plus));
            Assert.That(tokens[2].Name, Is.EqualTo(TokenKind.Power));
        }

        [Test]
        public void Tokenize_ReturnsTokens_WithBrackets()
        {
            string input = "x + (2 + y)";

            tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(input).ToList();

            Print(tokens);


            Assert.That(tokens[1].Name, Is.EqualTo(TokenKind.Plus));
            Assert.That(tokens[2].Name, Is.EqualTo(TokenKind.LParen));
        }
      
        private static void Print(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine("{0} : {1}", token.Name, token.Value);
            }
        }
    }
}
