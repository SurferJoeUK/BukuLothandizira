using System.Xml;

namespace AssetRegister.Attributes.MakeModel
{
	public class MakeModel
	{
		public MakeModel(string make, string model)
		{
			this.Make = make;
			this.Model = model;
		}

		public string Model { get; set; }

		public string Make { get; set; }

		public void WriteXml(XmlWriter writer, string elementName)
		{
			writer.WriteStartElement(elementName);
			writer.WriteElementString(nameof(this.Make), this.Make);
			writer.WriteElementString(nameof(this.Model), this.Model);
			writer.WriteEndElement();
		}
	}
}