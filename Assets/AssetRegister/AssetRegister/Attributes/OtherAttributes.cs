using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Attributes
{
	public interface IOtherAttribute
	{
	}

	class OtherAttribute : IOtherAttribute
	{
		public KeyValuePair<string, string> KVP { get; set; }
		public OtherAttribute(string title, string value)
		{
			this.KVP = new KeyValuePair<string, string>(title, value);
		}
	}
}
