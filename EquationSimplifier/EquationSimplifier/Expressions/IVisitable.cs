using EquationSimplification.Visitors;

namespace EquationSimplification.Expressions
{
	public interface IVisitable
	{
		void Accept(IVisitor visitor);
	}
}