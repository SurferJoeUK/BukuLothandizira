using System;

namespace AssetRegister.Attributes.AssetTag
{
	public class DuplicatedAssetTag : IAssetTag
	{
		public DuplicatedAssetTag(string data)
		{
			AssetId = Guid.NewGuid();
			AssetTag = data;
		}

		public Guid AssetId { get; set; }
		public string AssetTag { get; set; }

		public string ToDisplayString()
		{
			return AssetTag;
		}
	}
}