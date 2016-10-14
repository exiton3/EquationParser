using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public sealed class SumExpression : BinaryExpression
	{
		public SumExpression(IExpression left, IExpression right)
			: base(left, right)
		{
		}

		public override double Evaluate(IContext context)
		{
			return Left.Evaluate(context) + Right.Evaluate(context);
		}

	    public override IExpression Simplify()
	    {
	        return null;
	    }

	    public override void Accept(IVisitor visitor)
		{
			Left.Accept(visitor);
			visitor.Visit(this);
			Right.Accept(visitor);

		}
	}
}