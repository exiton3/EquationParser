using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public sealed class ConstantExpression : IExpression
	{
		private readonly double value;

		public ConstantExpression(double value)
		{
			this.value = value;
		}

		public double Value
		{
			get { return value; }
		}

		#region IdoubleeanExpression Members

		public double Evaluate(IContext context)
		{
			return value;
		}

		public IExpression Replace(string s, IExpression exp)
		{
			return null;
		}

		public IExpression Copy()
		{
			return new ConstantExpression(value);
		}

	    public IExpression Simplify()
	    {
	        throw new System.NotImplementedException();
	    }

	    public void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}

		#endregion
	}
}