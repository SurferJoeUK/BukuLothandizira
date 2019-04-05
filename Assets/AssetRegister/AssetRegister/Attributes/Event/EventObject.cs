using System;

namespace AssetRegister.Attributes.Event
{
	public class EventObject : IEvent
	{
		public EventObject(DateTime date, string title, bool isWarrantyEvent)
		{
			this.EventDate = date;
			this.Title = title;
			this.IsWarrantyEvent = isWarrantyEvent;
		}

		public DateTime? EventDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
	}

	public class DodgyDateEvent : IEvent
	{
		public DodgyDateEvent(string value, string colTitle, bool isWarrantyEvent)
		{
			DodgyDate = value;
			Title = colTitle;
			IsWarrantyEvent = isWarrantyEvent;
		}

		public DateTime? EventDate { get; set; } = null;
		public string DodgyDate { get; set; }
		public string Title { get; set; }
		public bool IsWarrantyEvent { get; set; }
	}
}