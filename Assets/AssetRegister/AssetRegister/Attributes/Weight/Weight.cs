using System.Xml;

namespace AssetRegister.Attributes.Weight
{
	public interface IWeight
	{
		void WriteXml(XmlWriter writer, string elementName, string type);
	}
	class WeightString:IWeight
	{
		public WeightString(string toString)
		{
			this.Weight = toString;
		}

		public string Weight { get; set; }
		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.Weight), this.Weight);
			writer.WriteEndElement();

		}
	}
}
