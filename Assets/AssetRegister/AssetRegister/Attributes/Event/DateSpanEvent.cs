using System;
using System.Xml;
using AssetRegister.Helpers;

namespace AssetRegister.Attributes.Event
{
	public class DateSpanEvent : IEvent
	{
		public DateSpanEvent(DateTime date1, DateTime date2, string colTitle, bool isWarrantyEvent)
		{
			// NOTE End date gets changed to be the milisecond before the next day
			Title = colTitle;
			IsWarrantyEvent = isWarrantyEvent;
			if (date2 >= date1)
			{
				EndDate = OrderBy = date2.UntilEndOfDay();
				StartDate = date1;
			}
			else
			{
				StartDate = date2;
				EndDate = OrderBy = date1.UntilEndOfDay();
			}
		}

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		//public DateTime? EventDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.StartDate), StartDate?.ToString(Constants.DateTimeFormat) ?? "NULL");
			writer.WriteAttributeString(nameof(this.EndDate), EndDate?.ToString(Constants.DateTimeFormat) ?? "NULL");
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}
}