﻿using System;
using System.Collections.Generic;
using AssetBlockchain;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace P2POps
{
	public class P2PServer : WebSocketBehavior
	{
		bool chainSynched = false;
		WebSocketServer wss = null;

		public void Start()
		{
			wss = new WebSocketServer($"ws://127.0.0.1:{PublicProgram.Port}");
			wss.AddWebSocketService<P2PServer>("/Blockchain");
			wss.Start();
			Console.WriteLine($"Started server at ws://127.0.0.1:{PublicProgram.Port}");
		}

		protected override void OnMessage(MessageEventArgs e)
		{
			if (e.Data == "Hi Server")
			{
				Console.WriteLine(e.Data);
				Send("Hi Client");
			}
			else
			{
				Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

				if (newChain.IsValid() && newChain.Chain.Count > PublicProgram.HandoverManuals.Chain.Count)
				{
					List<Transaction> newTransactions = new List<Transaction>();
					newTransactions.AddRange(newChain.PendingTransactions);
					newTransactions.AddRange(PublicProgram.HandoverManuals.PendingTransactions);

					newChain.PendingTransactions = newTransactions;
					PublicProgram.HandoverManuals = newChain;
				}

				if (!chainSynched)
				{
					Send(JsonConvert.SerializeObject(PublicProgram.HandoverManuals));
					chainSynched = true;
				}
			}
		}
	}
}
