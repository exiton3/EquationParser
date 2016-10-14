using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquationSimplification.Expressions;

namespace EquationSimplification.Visitors
{
   public class SimplificationVisitor : Visitor
    {
        readonly Dictionary<string,List<double>> variables = new Dictionary<string, List<double>>();

        readonly List<double> constants = new List<double>();

        public override void Visit(ConstantExpression expression)
        {
            constants.Add(expression.Value);
        }

        public override void Visit(VariableExpression expression)
        {
            if (variables.ContainsKey(expression.Name))
            {
                variables[expression.Name].Add(1);
            }
            else
            {
                variables.Add(expression.Name, new List<double> {1});
            }
        }

        public override void Visit(PowerExpression expression)
        {
            if (variables.ContainsKey(expression.Name))
            {
                variables[expression.Name].Add(expression.Coef);
            }
            else
            {
                variables.Add(expression.Name, new List<double> { expression.Coef});
            }
        }

        public string GetSimplifiedExp()
        {
            var stringBuilder = new StringBuilder();
            int count = 0;
            foreach (var variable in variables)
            {
                var coef = variable.Value.Sum();
                var k = coef.ToString();
                if (k == "1")
                {
                    k = string.Empty;
                }
                if (k == "-1")
                {
                    k = "-";
                }
                if (count > 0)
                {
                    stringBuilder.Append(coef > 0 ? " + " : " ");
                }
                count++;
                if (coef == 0)
                {
                    continue;
                }
                stringBuilder.AppendFormat("{0}{1}", k, variable.Key);
            }
            double constant = constants.Sum();
            if (constant == 0.0 && variables.Count == 0)
            {
                return  "0";
            }
            if (constant == 0.0 && variables.Count>0)
            {
                return stringBuilder.ToString();
            }

            if (constant > 0)
            {
            stringBuilder.AppendFormat(" + {0}", constant);

            }
            else
            {
                stringBuilder.AppendFormat(" {0}", constant);
            }
            return stringBuilder.ToString();
        }
    }
}