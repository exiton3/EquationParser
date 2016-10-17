using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EquationSimplification.Parser
{
    public class PowerParser
    {
        public IEnumerable<Member> Parse(string input)
        {
            var myRegex = new Regex(@"(?<Var>[a-z])([\^](?<Pow>\d+))?", RegexOptions.None);

            var members = new List<Member>();

            foreach (Match match in myRegex.Matches(input))
            {
                if (!match.Success) continue;

                var v = match.Groups["Var"].Value;
                var p = match.Groups["Pow"].Value;
                int power = 1;
                if (!string.IsNullOrEmpty(p))
                {
                    power = int.Parse(p);
                }
                var member = new Member {  Variable = v,Power = power};
                members.Add(member);
                
            }
            
            return members;
        }
    }
}