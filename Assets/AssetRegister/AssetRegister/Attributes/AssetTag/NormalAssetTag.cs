using System;
using System.Xml;
using System.Xml.Serialization;

namespace AssetRegister.Attributes.AssetTag
{
	public class DescriptiveAssetTag : IAssetTag
	{
		public DescriptiveAssetTag(string data)
		{
			AssetHeader = data;
		}
		public string AssetTag { get; set; } 
		public string AssetHeader { get; set; }
		public string ToDisplayString()
		{
			return AssetHeader;
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(nameof(this.AssetHeader), this.AssetHeader);
			writer.WriteEndElement();
		}
	}
	public class NormalAssetTag:IAssetTag
	{
		//private NormalAssetTag()
		//{
		//	// parameterless constructor for Xml
		//}
		public NormalAssetTag(string data)
		{
			AssetTag = data;
		}
			
		public string AssetTag { get; set; }

		public string ToDisplayString()
		{
			return AssetTag;
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(nameof(this.AssetTag), this.AssetTag);			
			writer.WriteEndElement();
		}
	}
}