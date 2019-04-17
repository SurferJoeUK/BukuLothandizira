using System;
using System.Xml;

namespace AssetRegister.Attributes.Event
{
	public class ProjectedEvent : IEvent
	{
		public enum TimeFrame
		{
			Years, Months, Weeks, Days
		}

		public ProjectedEvent(int numberOf, TimeFrame tf, string colTitle, bool isWarrantyEvent)
		{
			ProjectionLength = numberOf;
			TimePeriod = tf;
			Title = colTitle;
			IsWarrantyEvent = isWarrantyEvent;

			switch (tf)
			{
				case TimeFrame.Years:
					OrderBy = DateTime.Now.AddYears(numberOf);
					break;
				case TimeFrame.Months:
					OrderBy = DateTime.Now.AddMonths(numberOf);
					break;
				case TimeFrame.Weeks:
					OrderBy = DateTime.Now.AddDays(numberOf * 7);
					break;
				case TimeFrame.Days:
					OrderBy = DateTime.Now.AddDays(numberOf);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(tf), tf, null);
			}
		}

		public TimeFrame TimePeriod { get; set; }
		public int ProjectionLength { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
		public DateTime? OrderBy { get; set; }

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.ProjectionLength), this.ProjectionLength.ToString());
			writer.WriteAttributeString(nameof(this.TimePeriod), this.TimePeriod.ToString());
			writer.WriteAttributeString(nameof(this.Title), this.Title);
			writer.WriteAttributeString(nameof(this.IsWarrantyEvent), this.IsWarrantyEvent.ToString());
			writer.WriteEndElement();
		}
	}
}