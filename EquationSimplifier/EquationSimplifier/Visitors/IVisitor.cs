using EquationSimplification.Expressions;

namespace EquationSimplification.Visitors
{
	public interface IVisitor
	{
		void Visit(SumExpression expression);
		void Visit(MinusExpression expression);
		void Visit(ProductExpression expression);
		void Visit(ConstantExpression expression);
		void Visit(VariableExpression expression);
	    void Visit(PowerExpression expression);
	}
}