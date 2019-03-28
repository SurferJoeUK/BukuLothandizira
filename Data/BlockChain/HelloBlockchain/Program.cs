using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using AssetBlockchain;
using Newtonsoft.Json;

namespace HelloBlockchain
{
	class Program
	{
		static void Main(string[] args)
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
		static void Main2(string[] args)
		{
			Blockchain bcCegbu = new Blockchain();
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));
			

			Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));
			Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");

			Console.WriteLine("Add dodgy data");
			bcCegbu.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
			bcCegbu.Chain[1].Hash = bcCegbu.Chain[1].CalculateHash();

			Console.WriteLine(JsonConvert.SerializeObject(bcCegbu, Formatting.Indented));
			Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");

			Console.WriteLine($"Update the entire chain");
			bcCegbu.Chain[2].PreviousHash = bcCegbu.Chain[1].Hash;
			bcCegbu.Chain[2].Hash = bcCegbu.Chain[2].CalculateHash();
			bcCegbu.Chain[3].PreviousHash = bcCegbu.Chain[2].Hash;
			bcCegbu.Chain[3].Hash = bcCegbu.Chain[3].CalculateHash();
			Console.WriteLine($"Is Chain Valid: {bcCegbu.IsValid()}");


			var startTime = DateTime.Now;

			bcCegbu = new Blockchain();
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
			bcCegbu.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

			var endTime = DateTime.Now;

			Console.WriteLine($"Duration: {endTime - startTime}");

			Console.ReadLine();
		}
	}
}
