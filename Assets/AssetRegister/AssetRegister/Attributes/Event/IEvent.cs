using System;
using System.Xml;
using AssetRegister.Objects;

namespace AssetRegister.Attributes.Event
{
	public interface IEvent
	{
		
		string Title { get; set; }
		bool IsWarrantyEvent { get; set; }
		DateTime? OrderBy { get; set; }
		void WriteXml(XmlWriter writer, string elementName, string type);
	}

	public interface IKnownDateEvent : IEvent
	{
		//DateTime? EventDate { get; set; }
		DateTime? EventDate { get; set; }
	}
}