using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace AssetRegister.Attributes.AssetTag
{

	public class DuplicatedAssetTag : IAssetTag
	{
		//private DuplicatedAssetTag()
		//{
		//	// parameterless constructor for Xml
		//}
		public DuplicatedAssetTag(string data)
		{
			
			AssetTag = data;
		}
		
		public string AssetTag { get; set; }

		public string ToDisplayString()
		{
			return AssetTag;
		}

		public void WriteXml(XmlWriter writer,string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteElementString(nameof(this.AssetTag), this.AssetTag);
			writer.WriteEndElement();
		}
	}
}