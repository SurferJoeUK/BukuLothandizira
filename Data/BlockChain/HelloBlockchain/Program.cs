using System;
using Newtonsoft.Json;

namespace HelloBlockchain
{
	class Program
	{
		public static int Port = 0;
		public static Blockchain HandoverManuals = new Blockchain();
		public static string Name = "Unknown";
		public static P2PServer Server = null;
		public static P2PClient Client = new P2PClient();

		static void Main(string[] args)
		{
			HandoverManuals.InitializeChain();
			if (args.Length >= 1)
			{
				Port = Int32.Parse(args[0]);
			}

			if (args.Length >= 2)
			{
				Name = args[1];
			}

			if (Port > 0)
			{
				Server = new P2PServer();
				Server.Start();
			}

			if (Name != "Unknown")
			{
				Console.WriteLine($"Current user is {Name}");
			}

			Console.WriteLine("=======================");
			Console.WriteLine("1. Connect to a server");
			Console.WriteLine("2. Add a transaction");
			Console.WriteLine("3. Display Blockchain");
			Console.WriteLine("4. Exit");
			Console.WriteLine("=======================");

			int selection = 0;
			while (selection != 4)
			{
				switch (selection)
				{
					case 1:
					{
						Console.WriteLine("enter server url");
						string serverUrl = Console.ReadLine();
						Client.Connect($"{serverUrl}/Blockchain");
						break;
					}
					case 2:
					{
						Console.WriteLine("Please enter the make/model");
						string makeModel = Console.ReadLine();
						Console.WriteLine("Please enter the Asset ID");
						string assetId = Console.ReadLine();
						Console.WriteLine("Please enter the Document ID");
						string documentId = Console.ReadLine();
						Console.WriteLine("Please enter the Title");
						string title = Console.ReadLine();

						HandoverManuals.CreateTransaction(new Transaction(makeModel, assetId, documentId, title));
						HandoverManuals.ProcessPendingTransactions(Name);
						Client.Broadcast(JsonConvert.SerializeObject(HandoverManuals));
						break;
					}
					case 3:
					{
						Console.WriteLine("Blockchain");
						Console.WriteLine(JsonConvert.SerializeObject(HandoverManuals, Formatting.Indented));
						break;
					}
				}

				Console.WriteLine("Please select an action");
				string action = Console.ReadLine();
				if (action != null) selection = Int32.Parse(action);
			}

			Client.Close();

		}
		static void Main3(string[] args)
		{
			var startTime = DateTime.Now;

			Blockchain bcCegbu = new Blockchain();
			bcCegbu.CreateTransaction(new Transaction(null, "AST-123", "12345678", "Commissing Certificate"));
			bcCegbu.CreateTransaction(new Transaction("Ropey Lifts Ltd Liftomatic 5",null, "12345678", "Operating Manual"));
			bcCegbu.CreateTransaction(new Transaction("Weazey plc AHU-500", null, "234567890", "Electrical Diagram"));
			bcCegbu.ProcessPendingTransactions("Nige");
			Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));

			bcCegbu.CreateTransaction(new Transaction(null, "AST-127", "11133333", "Asset 127 Document"));
			bcCegbu.CreateTransaction(new Transaction(null, "AST-128", "12355555", "Asset 128 Document"));
			bcCegbu.ProcessPendingTransactions("Bill");

			var endTime = DateTime.Now;

			Console.WriteLine($"Duration: {endTime - startTime}");

			Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));
			Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");



			Console.ReadLine();
		}
		//static void Main2(string[] args)
		//{
		//	Blockchain bcCegbu = new Blockchain();
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));
			

		//	Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));
		//	Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");

		//	Console.WriteLine("Add dodgy data");
		//	bcCegbu.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
		//	bcCegbu.Chain[1].Hash = bcCegbu.Chain[1].CalculateHash();

		//	Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));
		//	Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");

		//	Console.WriteLine($"Update the entire chain");
		//	bcCegbu.Chain[2].PreviousHash = bcCegbu.Chain[1].Hash;
		//	bcCegbu.Chain[2].Hash = bcCegbu.Chain[2].CalculateHash();
		//	bcCegbu.Chain[3].PreviousHash = bcCegbu.Chain[2].Hash;
		//	bcCegbu.Chain[3].Hash = bcCegbu.Chain[3].CalculateHash();
		//	Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");


		//	var startTime = DateTime.Now;

		//	bcCegbu = new Blockchain();
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
		//	bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

		//	var endTime = DateTime.Now;

		//	Console.WriteLine($"Duration: {endTime - startTime}");

		//	Console.ReadLine();
		//}
	}
}
