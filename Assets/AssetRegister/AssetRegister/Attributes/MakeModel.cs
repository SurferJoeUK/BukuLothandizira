namespace AssetRegister.Attributes
{
	public class MakeModel
	{
		public MakeModel(string v1, string v2)
		{
			this.Make = v1;
			this.Model = v2;
		}

		public string Model { get; set; }

		public string Make { get; set; }
	}
}