using System.Xml;

namespace AssetRegister.Attributes.AssetCategory
{
	public interface IAssetCategory
	{
		string AssetCategory { get; set; }
		void WriteXml(XmlWriter writer, string elementName);
	}
}