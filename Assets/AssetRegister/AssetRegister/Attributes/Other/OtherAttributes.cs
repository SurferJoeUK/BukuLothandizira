using System.Collections.Generic;
using System.Xml;
using AssetRegister.Helpers;

namespace AssetRegister.Attributes.Other
{
	public interface IOtherAttribute
	{

		void WriteXml(XmlWriter writer, string elementName, string type);
	}

	class ServiceMaintenanceAttribute : OtherAttribute
	{
		public ServiceMaintenanceAttribute(string title, string value) : base(title, value)
		{
			this.KVP = new KeyValuePair<string, string>(title, value);
		}
	}
	class OtherAttribute : IOtherAttribute
	{
		public KeyValuePair<string, string> KVP { get; set; }
		public OtherAttribute(string title, string value)
		{
			this.KVP = new KeyValuePair<string, string>(title, value);
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(this.KVP.Key.GetSafeKey(), this.KVP.Value);
			writer.WriteEndElement();
		}
	}
}
