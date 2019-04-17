using System.Xml;

namespace AssetRegister.Attributes.Location
{
	public class BuildingLevelRoom : IAssetLocation
	{
		public BuildingLevelRoom(string bldg, string lvl, string nbr)
		{
			this.Building = bldg;
			this.Level = lvl;
			this.RoomNumber = nbr;
		}

		public string Building { get; set; }
		public string Level { get; set; }
		public string RoomNumber { get; set; }

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(nameof(this.Building), this.Building);
			writer.WriteElementString(nameof(this.Level), this.Level); 
			writer.WriteElementString(nameof(this.RoomNumber), this.RoomNumber); 
			writer.WriteEndElement();

		}
	}
}