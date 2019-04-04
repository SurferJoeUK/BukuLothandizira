namespace AssetRegister.Attributes.Location
{
	public interface IAssetLocation { }

	public class SimpleLocation : IAssetLocation
	{
		public string Location { get; set; }
	}

	public class BuildingLevelRoom : IAssetLocation
	{
		public BuildingLevelRoom(string bldg, string lvl, string nbr)
		{
			this.Building = bldg;
			this.Level = lvl;
			this.RoomNumber = nbr;
		}

		public string Building { get; set; }
		public string Level { get; set; }
		public string RoomNumber { get; set; }		

	}
}