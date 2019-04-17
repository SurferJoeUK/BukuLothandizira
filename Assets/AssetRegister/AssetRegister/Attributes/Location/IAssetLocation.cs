using System.Xml;

namespace AssetRegister.Attributes.Location
{
	public interface IAssetLocation {
		void WriteXml(XmlWriter writer, string elementName, string type);
	}
}