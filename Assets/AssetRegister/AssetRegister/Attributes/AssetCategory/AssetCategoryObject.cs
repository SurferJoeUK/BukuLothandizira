using System.Xml;

namespace AssetRegister.Attributes.AssetCategory
{
	public class AssetCategoryObject : IAssetCategory
	{
		public AssetCategoryObject()
		{
			// parameterless constructor for Xml
		}
		public AssetCategoryObject(string v)
		{
			this.AssetCategory = v;
		}

		public string AssetCategory { get; set; }
		public void WriteXml(XmlWriter writer, string elementName)
		{
			writer.WriteElementString(nameof(this.AssetCategory), this.AssetCategory);
		}
	}
}