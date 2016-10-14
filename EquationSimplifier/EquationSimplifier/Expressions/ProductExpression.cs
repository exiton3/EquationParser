using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public sealed class ProductExpression : BinaryExpression
	{
		public ProductExpression(IExpression left, IExpression right)
			: base(left, right)
		{
		}

		public override double Evaluate(IContext context)
		{
			return Left.Evaluate(context) * Right.Evaluate(context);
		}

	    public override IExpression Simplify()
	    {
	        throw new System.NotImplementedException();
	    }

	    public override void Accept(IVisitor visitor)
		{
			Left.Accept(visitor);
			visitor.Visit(this);
			Right.Accept(visitor);

		}
	}
}