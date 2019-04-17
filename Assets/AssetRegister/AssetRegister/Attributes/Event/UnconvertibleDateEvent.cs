using System;
using System.Xml;

namespace AssetRegister.Attributes.Event
{
	public class UnconvertibleDateEvent : IEvent
	{
		public UnconvertibleDateEvent(string value, string colTitle, bool isWarrantyEvent)
		{
			//EventDate = null;
			UnconvertibleDate = value;
			Title = colTitle;
			IsWarrantyEvent = isWarrantyEvent;
			OrderBy = null;
		}

		//public DateTime? EventDate { get; set; } = null;
		public string UnconvertibleDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.UnconvertibleDate), this.UnconvertibleDate);
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}
}