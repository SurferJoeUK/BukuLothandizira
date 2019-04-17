using System.Xml;

namespace AssetRegister.Attributes.Discipline
{
	public interface IDiscipline
	{
		string Name { get; set; }
		void WriteXml(XmlWriter writer, string elementName, string type);
	}
}