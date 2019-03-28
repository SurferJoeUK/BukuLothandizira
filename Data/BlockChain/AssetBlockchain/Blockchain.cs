using System;
using System.Collections.Generic;

namespace AssetBlockchain
{
	public class Blockchain
	{
		IList<Transaction> PendingTransactions = new List<Transaction>();
		public int Difficulty { set; get; } = 2;
		
		public IList<Block> Chain { set; get; }

		public Blockchain()
		{
			InitializeChain();
			AddGenesisBlock();
		}


		public void InitializeChain()
		{
			Chain = new List<Block>();
		}

		public Block CreateGenesisBlock()
		{
			return new Block(DateTime.Now, null, "{}");
		}

		public void AddGenesisBlock()
		{
			Chain.Add(CreateGenesisBlock());
		}

		public Block GetLatestBlock()
		{
			return Chain[Chain.Count - 1];
		}

		public void AddBlock(Block block)
		{
			Block latestBlock = GetLatestBlock();
			block.Index = latestBlock.Index + 1;
			block.PreviousHash = latestBlock.Hash;
			block.Mine(this.Difficulty);
			Chain.Add(block);
		}

		public void ProcessPendingTransactions(string minerAddress)
		{
			Block block = new Block(DateTime.Now, GetLatestBlock().Hash, this.PendingTransactions);
			AddBlock(block);

			PendingTransactions = new List<Transaction>();
			CreateTransaction(new Transaction(null, "XYZ-987", "DOC-11", "As-installed Photograph"));
		}

		public bool IsValid()
		{
			for (int i = 1; i < Chain.Count; i++)
			{
				Block currentBlock = Chain[i];
				Block previousBlock = Chain[i - 1];

				if (currentBlock.Hash != currentBlock.CalculateHash())
				{
					return false;
				}

				if (currentBlock.PreviousHash != previousBlock.Hash)
				{
					return false;
				}
			}
			return true;
		}

		public void CreateTransaction(Transaction transaction)
		{
			PendingTransactions.Add(transaction);
		}
	}
}