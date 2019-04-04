namespace AssetRegister.Attributes.AssetType
{
	public class AssetTypeObject : IAssetType
	{
		public AssetTypeObject(string v)
		{
			this.AssetType = v;
		}

		public string AssetType { get; set; }
	}
}