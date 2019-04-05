using System;

namespace AssetRegister.Attributes.Event
{
	public interface IEvent
	{
		DateTime? EventDate { get; set; }
		string Title { get; set; }
		bool IsWarrantyEvent { get; set; }
	}
}