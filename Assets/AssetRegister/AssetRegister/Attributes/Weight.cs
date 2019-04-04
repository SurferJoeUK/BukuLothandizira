using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Attributes
{
	public interface IWeight
	{
		
	}
	class WeightString:IWeight
	{
		public WeightString(string toString)
		{
			this.Weight = toString;
		}

		public string Weight { get; set; }
	}
}
