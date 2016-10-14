using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public abstract class BinaryExpression : IExpression
	{
		protected IExpression Left;
		protected IExpression Right;

		protected BinaryExpression(IExpression left, IExpression right)
		{
			Left = left;
			Right = right;
		}

		#region IBooleanExpression Members

		public abstract double Evaluate(IContext context);

		public IExpression Replace(string s, IExpression exp)
		{
			return new ProductExpression(Left.Replace(s, exp), Right.Replace(s, exp));
		}

		public IExpression Copy()
		{
			return new ProductExpression(Left.Copy(), Right.Copy());
		}

	    public abstract IExpression Simplify();
	   

	    public abstract void Accept(IVisitor visitor);

		#endregion
	}
}