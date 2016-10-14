namespace EquationSimplification.Expressions
{
	public interface IExpression : IVisitable
	{
		double Evaluate(IContext context);
		IExpression Replace(string s, IExpression exp);
		IExpression Copy();

	    IExpression Simplify();
	}
}
