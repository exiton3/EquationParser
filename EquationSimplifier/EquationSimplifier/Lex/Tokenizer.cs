using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EquationSimplifier.Lex
{
    public class Tokenizer : ITokenizer
    {
        private readonly Dictionary<TokenKind, Regex> patterns;
        private readonly Stack<Token> tokens;

        public Tokenizer()
        {
            patterns = new Dictionary<TokenKind, Regex>
                           {
                               {TokenKind.Constant, new Regex(@"^([0|1]{1})|true|false$")},
                               {TokenKind.And, new Regex(@"^&|and$")},
                               {TokenKind.Or, new Regex(@"^[\|]{2}|or$")},
                               {TokenKind.Not, new Regex(@"^[!]{1}|not$")},
                               {TokenKind.Variable, new Regex(@"^[\w]+$")},
                               {TokenKind.LParen, new Regex(@"^\($")},
                               {TokenKind.RParen, new Regex(@"^\)$")},
                           };
            tokens = new Stack<Token>();
        }

        #region ITokenizer Members



        public IEnumerable<Token> Tokenize(string input)
        {
            var strs = GetInputStrings(input);
            for (int i = strs.Length - 1; i >= 0; i--)
            {
                string str = strs[i];
                string value = str;
                Token token = patterns.Where(pattern => pattern.Value.IsMatch(value))
                    .Select(pattern => new Token
                    {
                        Name = pattern.Key,
                        Value = value
                    }).FirstOrDefault();
                if (token != null)
                {
                    tokens.Push(token);
                }
            }
            return tokens;
        }

        private string[] GetInputStrings(string input)
        {
            string resString = input.Replace("(", "( ").Replace(")", " )");
            return resString.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public Token GetNextToken()
        {
            if (tokens.Count == 0)
            {
                return null;
            }
            return tokens.Pop();
        }

        public void PushBack(Token token)
        {
            tokens.Push(token);
        }

        #endregion
    }
}