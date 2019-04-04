using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Attributes
{
	public interface IWarranty
	{	
		void AddDetails(object obj);
	}

	public class WarrantyBasic : IWarranty
	{
		public List<KeyValuePair<string, string>> Details { get; set; }

		public void AddDetails(string key, string value)
		{
			if (this.Details == null)
			{
				this.Details=new List<KeyValuePair<string, string>>();
			}

			this.Details.Add(new KeyValuePair<string, string>(key, value));
		}

		public void AddDetails(object obj)
		{
			if (obj is KeyValuePair<string, string> kvp)
			{
				this.Details.Add(kvp);
			}
		}
	}
}
