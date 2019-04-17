using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using AssetRegister.Attributes.AssetCategory;
using AssetRegister.Attributes.AssetTag;
using AssetRegister.Attributes.Discipline;
using AssetRegister.Attributes.Event;
using AssetRegister.Attributes.Location;
using AssetRegister.Attributes.MakeModel;
using AssetRegister.Attributes.Other;
using AssetRegister.Attributes.PhysicalTags;
using AssetRegister.Attributes.Supplier;
using AssetRegister.Attributes.Warranty;
using AssetRegister.Attributes.Weight;
using AssetRegister.Helpers;

namespace AssetRegister
{
	public class Asset
	{
		public Asset()
		{
		}

		public Asset(Guid guid)
		{
			this.AssetId = guid;
		}

		public void WriteXml(XmlWriter writer)
		{
			#region AssetTag

			writer.WriteStartElement("AssetId");
			writer.WriteAttributeString("AssetIdType", this.AssetId.GetType().AssemblyQualifiedName);
			XmlSerializer xmlSerializer = new XmlSerializer(this.AssetId.GetType());
			xmlSerializer.Serialize(writer, this.AssetId);
			writer.WriteEndElement();

			#endregion AssetTag
		}

		internal void WriteXmlPhysicalTags(XmlWriter writer, string elementName, string type)
		{

			writer.WriteStartElement("PhysicalTagCollection");
			if (this.PhysicalTags != null)
			{
				foreach (var pt in this.PhysicalTags)
				{
					pt.WriteXml(writer, elementName, type);
				}
			}

			writer.WriteEndElement();
		}

		public void WriteXmlEventLog(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement("EventLogCollection");
			if (this.EventLog != null)
			{
				foreach (var el in this.EventLog.OrderBy(e => e.OrderBy))
				{
					el.WriteXml(writer, elementName, type);
				}
			}

			writer.WriteEndElement();
		}


		public void WriteXmlOtherAttributes(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement("OtherAttributes");
			if (this.OtherAttributes != null)
			{
				foreach (var oth in this.OtherAttributes)
				{
					oth.WriteXml(writer, elementName, type);
				}
			}

			writer.WriteEndElement();
		}

		public Guid AssetId { get; set; }
		public IAssetTag AssetTagDetail { get; set; }

		public IAssetCategory AssetCategory { get; set; }

		//public IAssetCategory Category { get; set; }
		public IAssetLocation Location { get; set; }
		public List<IPhysicalTag> PhysicalTags { get; set; }
		public List<IEvent> EventLog { get; set; }
		public IDiscipline Discipline { get; set; }
		public MakeModel MakeModel { get; set; }
		public Supplier Supplier { get; set; }
		public string Description { get; set; }
		public IWeight Weight { get; set; }
		public List<IOtherAttribute> OtherAttributes { get; set; }
		public IWarranty Warranty { get; set; }

		public void InitialiseEventLog()
		{
			if (this.EventLog == null)
			{
				this.EventLog = new List<IEvent>();
			}
		}

		public void InitialisePhysicalTags()
		{
			if (this.PhysicalTags == null)
			{
				this.PhysicalTags = new List<IPhysicalTag>();
			}
		}



		public void InitialiseOtherAttributes()
		{
			if (this.OtherAttributes == null)
			{
				this.OtherAttributes = new List<IOtherAttribute>();
			}
		}

		public void InitialiseWarranty()
		{
			if (this.Warranty == null)
			{
				this.Warranty = new WarrantyBasic();
			}
		}

		public void AddToEventLog_ReoccurringEvent(ReoccurringEvent.TimePeriod timePeriod, string colTitle,
			bool isWarrantyEvent = false)
		{
			this.EventLog.Add(new ReoccurringEvent(timePeriod, colTitle, isWarrantyEvent));
		}

		public void AddToEventLog_ProjectedEvent(string colValueOrig, string colTitle, bool isWarrantyEvent = false)
		{
			var colValue = colValueOrig.ToLower();

			if (Constants.Years.Any(colValue.Contains) || int.TryParse(colValue.Trim(), out int noOfYears))
			{

				foreach (string y in Constants.Years)
				{
					colValue = colValue.Replace(y, "");
				}


				if (int.TryParse(colValue.Trim(), out noOfYears))
				{
					this.EventLog.Add(new ProjectedEvent(noOfYears, ProjectedEvent.TimeFrame.Years, colTitle, isWarrantyEvent));
				}
				else
				{
					this.EventLog.Add(new UnconvertibleDateEvent(colValue, colTitle, isWarrantyEvent));
				}
			}
			else if (Constants.Months.Any(colValue.Contains))
			{
				foreach (string m in Constants.Months)
				{
					colValue = colValue.Replace(m, "");
				}

				if (int.TryParse(colValue.Trim(), out int noOfMonths))
				{
					this.EventLog.Add(new ProjectedEvent(noOfMonths, ProjectedEvent.TimeFrame.Months, colTitle, isWarrantyEvent));
				}
				else
				{
					this.EventLog.Add(new UnconvertibleDateEvent(colValue, colTitle, isWarrantyEvent));
				}
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}
		}


		public void AddToEventLog(string value, string colTitle, bool isWarrantyEvent = false)
		{
			try
			{
				this.EventLog.Add(new EventObject(Convert.ToDateTime(value), colTitle, isWarrantyEvent));
			}
			catch
			{
				try
				{
					// maybe the value is a time span
					var dates = value.Split('-');
					if (dates.Length == 2)
					{
						DateTime startDate = Convert.ToDateTime(dates[0].Trim());
						DateTime endDate = Convert.ToDateTime(dates[1].Trim());

						this.EventLog.Add(new DateSpanEvent(startDate, endDate, colTitle, isWarrantyEvent));
					}
					else
					{
						throw new Exception();
					}
				}
				catch
				{
					if (int.TryParse(value, out int result))
					{
						this.EventLog.Add(new EventObject(ExcelDateHelper.GetDate(value), colTitle, isWarrantyEvent));
					}
					else
					{
						this.EventLog.Add(new UnconvertibleDateEvent(value, colTitle, isWarrantyEvent));
					}
				}
			}
		}

		public void AddWarrantyDetail(DateTime value, string colTitle)
		{
			if (this.Warranty.GetType() == typeof(WarrantyBasic))
			{
				this.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value.ToShortDateString()));
			}
		}



		public void AddWarrantyDetail(string value, string colTitle)
		{

			if (this.Warranty.GetType() == typeof(WarrantyBasic))
			{
				this.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value));
			}
		}

		public void AddOtherAttribute(string otherAttributeHeader, string colTitle, string value, DataRow row)
		{
			this.OtherAttributes.Add(new OtherAttribute(otherAttributeHeader, row[colTitle].ToString()));

		}

		public void AddOtherAttribute(string colTitle, string value, DataRow row)
		{
			this.OtherAttributes.Add(new OtherAttribute(colTitle, row[colTitle].ToString()));

		}
	}
}