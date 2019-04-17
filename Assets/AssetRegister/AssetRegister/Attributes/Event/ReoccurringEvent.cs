using System;
using System.Xml;

namespace AssetRegister.Attributes.Event
{
	public class ReoccurringEvent : IEvent
	{
		public enum TimePeriod
		{
			Yearly, Quarterly, HalfYearly, Monthly, Weekly, Daily
		}

		public ReoccurringEvent(TimePeriod tp, string colTitle, bool isWarrantyEvent)
		{
			Reocurrance = tp;
			Title = colTitle;
			IsWarrantyEvent = isWarrantyEvent;
		}

		public TimePeriod Reocurrance { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }
		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.Reocurrance), this.Reocurrance.ToString());
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}
}