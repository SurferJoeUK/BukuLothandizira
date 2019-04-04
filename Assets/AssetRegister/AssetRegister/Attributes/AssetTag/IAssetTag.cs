using System;

namespace AssetRegister.Attributes.AssetTag
{
	public interface IAssetTag
	{
		Guid AssetId { get; set; }
		string AssetTag { get; set; }
		string ToDisplayString();
	}
}