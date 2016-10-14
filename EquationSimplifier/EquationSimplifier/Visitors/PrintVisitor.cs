using System;
using EquationSimplification.Expressions;

namespace EquationSimplification.Visitors
{
	public class PrintVisitor : IVisitor
	{

		public void Visit(SumExpression expression)
		{
			Console.Write("+ ");

		}

	    public void Visit(MinusExpression expression)
	    {
            Console.Write("- ");
        }

	    public void Visit(ProductExpression expression)
		{
			Console.Write("* ");

		}

		public void Visit(ConstantExpression expression)
		{
			Console.Write("{0} ", expression.Value);

		}

		public void Visit(VariableExpression expression)
		{
			Console.Write("{0} ", expression.Name);

		}

	    public void Visit(PowerExpression expression)
	    {
	        Console.Write("{0}{1}", expression.Coef, expression.Name);
	    }
	}
}