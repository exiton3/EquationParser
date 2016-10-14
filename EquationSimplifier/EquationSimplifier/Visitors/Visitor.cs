using EquationSimplification.Expressions;

namespace EquationSimplification.Visitors
{
	public class Visitor : IVisitor
	{
		public virtual void Visit(SumExpression expression)
		{
			
		}

	    public void Visit(MinusExpression expression)
	    {
	        
	    }

	    public virtual void Visit(ProductExpression expression)
		{
		}

		public virtual void Visit(ConstantExpression expression)
		{
		}

		public virtual void Visit(VariableExpression expression)
		{
		}

	    public virtual void Visit(PowerExpression expression)
	    {
	      
	    }
	}
}