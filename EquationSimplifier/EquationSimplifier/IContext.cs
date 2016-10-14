namespace EquationSimplification
{
	public interface IContext
	{
		double Lookup(string s);
		void Assign(string name, double value);
	}
}