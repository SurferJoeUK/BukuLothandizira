namespace AssetRegister.Attributes.Discipline
{
	public class DisciplineObject : IDiscipline
	{
		public DisciplineObject(string v)
		{
			this.Name = v;
		}

		public string Name { get; set; }
	}
}