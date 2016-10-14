using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EquationSimplification.Lex;

namespace EquationSimplification.Parser
{
    public class EquationParser : IEquationParser
    {
       private readonly ITokenizer tokenizer;

        public EquationParser(ITokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public IEnumerable<EquationMember> Parse(string inputString)
        {
            tokenizer.Tokenize(inputString);

            return ParseExpression();
        }

        private Token token;

        private double multiplier = 1;


        public IEnumerable<EquationMember> ParseExpression()
        {
            var regex = new Regex(@"((?<Koef>[\+-]?[\d\.]*)(?<Var>[a-z\^\d]+))");

            var equationMembers = new List<EquationMember>();

             token = tokenizer.GetNextToken();
            var lastTokenKind = TokenKind.Plus;

            while (token != null)
            {
                if (token.Name == TokenKind.LParen)
                {
                     multiplier = lastTokenKind == TokenKind.Minus ? -1 : 1;
                    var members = ParseExpression();

                    equationMembers.AddRange(members.Select(x => new EquationMember
                    {
                        Coef = x.Coef*multiplier,
                        Parameter = x.Parameter
                    }).ToList());
                    GetNextToken();
                    continue;
                }

                if (token.Name == TokenKind.RParen)
                {
                    return equationMembers;
                }

                if (token.Name == TokenKind.Minus)
                {
                    lastTokenKind = TokenKind.Minus;
                    GetNextToken();
                    continue;
                }

                if (token.Name == TokenKind.Plus)
                {
                    lastTokenKind = TokenKind.Plus;
                    GetNextToken();
                    continue;
                }


                if (token.Name == TokenKind.Constant)
                {
                    var val = double.Parse(token.Value);
                    if (lastTokenKind == TokenKind.Minus)
                    {
                        val *= -1;
                    }
                    equationMembers.Add(new EquationMember {Coef = val, Parameter = "Const"});
                    GetNextToken();
                    continue;
                }

                if (token.Name == TokenKind.Power)
                {
                    var match = regex.Match(token.Value);
                    var parameter = match.Groups["Var"].Value;
                    var value = match.Groups["Koef"].Value;
                    double coef;

                    if (value == "-")
                    {
                        coef = -1;
                    }
                    else
                    {
                        coef = string.IsNullOrEmpty(value) ? 1 : double.Parse(value);
                    }

                    if (lastTokenKind == TokenKind.Minus)
                    {
                        coef *= -1;
                    }

                    equationMembers.Add(new EquationMember {Coef = coef, Parameter = parameter});
                }

                GetNextToken();
            }
            
            return equationMembers;
        }
        private Token GetNextToken()
        {
            return token = tokenizer.GetNextToken();
        }
    }
}