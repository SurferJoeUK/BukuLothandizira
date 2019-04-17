using System.Xml;

namespace AssetRegister.Attributes.Discipline
{
	public class DisciplineObject : IDiscipline
	{
		public DisciplineObject(string discipline)
		{
			this.Name = discipline;
		}

		public string Name { get; set; }
		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteElementString(elementName, this.Name);
		}
	}
}