using System.Collections.Generic;

namespace EquationSimplification.Parser
{
    public interface IEquationParser
    {
        IEnumerable<EquationMember> Parse(string inputString);
        IEnumerable<EquationMember> ParseExpression();
    }
}