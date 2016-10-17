using System.Linq;
using System.Text;

namespace EquationSimplification.Parser
{
    public class EquationMember
    {
        private string Const = "Const";

        public EquationMember(string parameter, double coef)
        {
            Parameter = parameter;
            Coef = coef;
        }

        public string Parameter { get; private set; }

        public double Coef { get; private set; }

        public void Normalize()
        {
            if (Parameter == Const)
            {
                return;
            }
            var powerParser = new PowerParser();

            var result = powerParser.Parse(Parameter).ToList()
                .OrderByDescending(x => x.Power)
                .ThenBy(x => x.Variable);

            var stringBuilder = new StringBuilder();

            foreach (var member in result)
            {
                if (member.Power == 1)
                {
                    stringBuilder.Append(member.Variable);
                }
                else
                {
                    stringBuilder.AppendFormat("{0}^{1}", member.Variable, member.Power);
                }
            }

            Parameter = stringBuilder.ToString();
        }
    }
}