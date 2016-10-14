using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public sealed class VariableExpression : IExpression
	{
		private readonly string name;

		public VariableExpression(string name)
		{
			this.name = name;
		}

		public double Evaluate(IContext context)
		{
			return context.Lookup(name);
		}

		public IExpression Replace(string s, IExpression exp)
		{
			if (s == name)
			{
				return exp.Copy();
			}
			return Copy();
		}

		public IExpression Copy()
		{
			return new VariableExpression(name);
		}

	    public IExpression Simplify()
	    {
	        throw new System.NotImplementedException();
	    }

	    public void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}

		public string Name { get { return name; } }
	}
}