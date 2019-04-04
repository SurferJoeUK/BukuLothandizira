namespace HelloBlockchain
{
	public class Transaction
	{
		public string AssetId { get; set; }
		public string DocumentId { get; set; }
		public string Title { get; set; }
		public string MakeModel { get; set; }
		public Transaction(string makemodel, string assetId, string documentId, string title)
		{
			AssetId = assetId;
			DocumentId = documentId;
			Title = title;
			MakeModel = makemodel;
		}
	}
}
