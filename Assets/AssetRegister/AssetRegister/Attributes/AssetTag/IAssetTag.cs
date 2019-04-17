using System;
using System.Xml;

namespace AssetRegister.Attributes.AssetTag
{
	public interface IAssetTag
	{
		string AssetTag { get; set; }
		string ToDisplayString();
		void WriteXml(XmlWriter writer, string elementName, string type);
	}
}