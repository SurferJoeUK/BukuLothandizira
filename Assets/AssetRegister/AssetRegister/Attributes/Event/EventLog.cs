using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Attributes.Event
{
	public class EventLog:List<IEvent>
	{

	}

	public interface IEvent
	{
		DateTime EventDate { get; set; }
		string Title { get; set; }
	}

	public class EventObject : IEvent
	{
		public EventObject(DateTime date, string title)
		{
			this.EventDate = date;
			this.Title = title;
		}

		public DateTime EventDate { get; set; }
		public string Title { get; set; }
	}

}
