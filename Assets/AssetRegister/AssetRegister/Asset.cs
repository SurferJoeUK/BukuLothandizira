using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AssetRegister.Attributes;
using AssetRegister.Attributes.AssetTag;
using AssetRegister.Attributes.AssetType;
using AssetRegister.Attributes.Discipline;
using AssetRegister.Attributes.Event;
using AssetRegister.Attributes.Location;
using AssetRegister.Attributes.PhysicalTags;

namespace AssetRegister
{
	public class Asset
    {
	    public Asset()
	    {
			this.PhysicalTags=new List<IPhysicalTag>();
		    this.EventLog = new List<IEvent>();
	    }

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
    }
}
