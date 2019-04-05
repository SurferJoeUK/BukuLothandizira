using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AssetRegister.Attributes;
using AssetRegister.Attributes.AssetTag;
using AssetRegister.Attributes.AssetType;
using AssetRegister.Attributes.Discipline;
using AssetRegister.Attributes.Event;
using AssetRegister.Attributes.Location;
using AssetRegister.Attributes.PhysicalTags;
using AssetRegister.Helpers;

namespace AssetRegister
{
	public class Asset
    {
	    public IAssetTag AssetId { get; set; }
		public IAssetType AssetType { get; set; }
		public IAssetCategory Category { get; set; }
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
	    private void InitialiseWarranty()
	    {
			if (this.Warranty == null)
			{
				this.Warranty = new WarrantyBasic();
			}
		}

		public void AddToEventLog(string value, string colTitle, bool isWarrantyEvent=false)
	    {
		    this.InitialiseEventLog();
			try
		    {
			    this.EventLog.Add(new EventObject(Convert.ToDateTime(value), colTitle, isWarrantyEvent));
		    }
		    catch
		    {
			    if (int.TryParse(value, out int result))
			    {
				    this.EventLog.Add(new EventObject(ExcelDateHelper.GetDate(value), colTitle, isWarrantyEvent));
			    }
			    else
			    {
				    this.EventLog.Add(new DodgyDateEvent(value, colTitle, isWarrantyEvent));
			    }
		    }
		}

	    public void AddWarrantyDetail(DateTime value, string colTitle)
	    {
		    this.InitialiseWarranty();
		    if (this.Warranty.GetType() == typeof(WarrantyBasic))
		    {
			    this.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value.ToShortDateString()));
		    }
	    }



	    public void AddWarrantyDetail(string value, string colTitle)
	    {
			this.InitialiseWarranty();
		    if (this.Warranty.GetType() == typeof(WarrantyBasic))
		    {
			    this.Warranty.AddDetails(new KeyValuePair<string, string>(colTitle, value));
		    }
		}

	    public void AddOtherAttribute(string colTitle, string value, DataRow row)
	    {
			if (this.OtherAttributes == null)
		    {
			    this.OtherAttributes = new List<IOtherAttribute>();
		    }
		    this.OtherAttributes.Add(new OtherAttribute(colTitle, row[colTitle].ToString()));

		}
	}
}
