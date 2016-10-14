using System.Text.RegularExpressions;
using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
    public class PowerExpression : IExpression
    {
        private readonly double coef;

        private readonly string name;

        public PowerExpression( string expression)
        {
            var regex = new Regex(@"((?<Koef>[\+-]?[\d\.]*)(?<Var>[a-z\^\d]+))");

            var match = regex.Match(expression);

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
            this.coef = coef;
            name = parameter;
        }

        public double Coef { get { return coef; } }
        public string Name { get { return name; } }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double Evaluate(IContext context)
        {
            return coef;
        }

        public IExpression Replace(string s, IExpression exp)
        {
            throw new System.NotImplementedException();
        }

        public IExpression Copy()
        {
            throw new System.NotImplementedException();
        }

        public IExpression Simplify()
        {
            throw new System.NotImplementedException();
        }
    }
}