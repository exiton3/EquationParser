using System;
using System.Collections.Generic;

namespace EquationSimplification
{
	public class Context : IContext
	{
		private readonly IDictionary<string, double> contextDic;

		public Context()
		{
			contextDic = new Dictionary<string, double>();
		}
		public double Lookup(string s)
		{
			return contextDic[s];
		}

		public void Assign(string name, double value)
		{
			if (name == null) throw new ArgumentNullException("name");

			double val;
			if (contextDic.TryGetValue(name, out val))
			{
				contextDic[name] = value;
			}
			else
			{
				contextDic.Add(name, value);
			}
		}
	}
}