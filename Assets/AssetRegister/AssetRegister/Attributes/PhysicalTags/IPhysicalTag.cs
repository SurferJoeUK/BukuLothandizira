using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssetRegister.Attributes.PhysicalTags
{
	public interface IPhysicalTag
	{
		void WriteXml(XmlWriter writer, string elementName, string type);
	}
}
