using System.Collections.Generic;

namespace EquationSimplifier.Lex
{
    public interface ITokenizer
    {
        Token GetNextToken();
        IEnumerable<Token> Tokenize(string input);
        void PushBack(Token token);
    }
}