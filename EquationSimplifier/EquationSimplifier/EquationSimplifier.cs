using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EquationSimplification.Parser;

namespace EquationSimplification
{
    public class EquationSimplifier
    {
        private readonly IEquationParser parser;

        public EquationSimplifier(IEquationParser parser)
        {
            this.parser = parser;
        }

        public string Simplify(string equationString)
        {
            if (equationString == null) throw new ArgumentNullException(nameof(equationString));

            var strings = equationString.Split('=');
            if (strings.Length != 2)
            {
                throw new ArgumentException("Input equation in wrong format",nameof(equationString));
            }
            var leftPart = strings[0];
            var rightPart = strings[1];

            var equationMembers = parser.Parse(leftPart);
            var membersRightPart= parser.Parse(rightPart)
                .Select(x=> new EquationMember {Parameter = x.Parameter, Coef = x.Coef*-1});
           equationMembers = equationMembers.Union(membersRightPart);

            var parsedTokens = Simplify(equationMembers);

            var members = new List<EquationMember>();

            foreach (var parsedToken in parsedTokens)
            {
                var sum = parsedToken.Value.Sum();
                if (sum != 0.0)
                {
                    members.Add( new EquationMember
                    {
                        Coef = sum,
                        Parameter = parsedToken.Key
                    });
                }
            }
            
            var stringBuilder = ConstructEquation(members);

            return stringBuilder;
        }

        private static Dictionary<string, List<double>> Simplify(IEnumerable<EquationMember> equationMembers)
        {
            var parsedTokens = new Dictionary<string, List<double>>();

            foreach (var member in equationMembers)
            {
                if (parsedTokens.ContainsKey(member.Parameter))
                {
                    parsedTokens[member.Parameter].Add(member.Coef);
                }
                else
                {
                    parsedTokens.Add(member.Parameter, new List<double> {member.Coef});
                }
            }
            return parsedTokens;
        }

        private static string ConstructEquation(IEnumerable<EquationMember> parsedTokens)
        {
            var stringBuilder = new StringBuilder();
            var equationMembers =  parsedTokens.ToList();
            if (!equationMembers.Any())
            {
                return "0 = 0";
            }
            foreach (var parsedToken in equationMembers)
            {
                var coef = parsedToken.Coef;
                var k = coef.ToString(CultureInfo.InvariantCulture);
                if (parsedToken.Parameter != "Const")
                {
                    if (k == "1")
                    {
                        k = string.Empty;
                    }
                    if (k == "-1")
                    {
                        k = "-";
                    }
                }
                stringBuilder.Append(coef > 0 ? " + " : " ");
                if (parsedToken.Parameter == "Const")
                {
                    stringBuilder.AppendFormat("{0}", k);
                }
                else
                {
                    stringBuilder.AppendFormat("{0}{1}", k, parsedToken.Parameter);
                }

            }
            stringBuilder.Append(" = 0");
            return stringBuilder.ToString().Trim(' ', '+');
        }
    }
}