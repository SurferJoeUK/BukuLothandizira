using System;
using System.Security.Policy;
using System.Xml;
using AssetRegister.Helpers;
using AssetRegister.Objects;

namespace AssetRegister.Attributes.Event
{
	public class PartialDateEvent : IEvent
	{
		private NotDateTime EventDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }

		public PartialDateEvent(NotDateTime date, string title, bool isWarrantyEvent)
		{
			this.EventDate = date;
			this.OrderBy = date.AsDateTime();
			this.Title = title;
			this.IsWarrantyEvent = isWarrantyEvent;
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.EventDate), "");
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}

	public class EventObject : IKnownDateEvent
	{
		public EventObject(DateTime date, string title, bool isWarrantyEvent)
		{
			this.EventDate= this.OrderBy = date;
			this.Title = title;
			this.IsWarrantyEvent = isWarrantyEvent;
		}

		public DateTime? EventDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);

			writer.WriteAttributeString(nameof(this.EventDate),
				EventDate?.ToString(Constants.DateTimeFormat) ?? "NULL");
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}
}