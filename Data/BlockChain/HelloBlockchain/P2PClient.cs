using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebSocketSharp;


namespace HelloBlockchain
{
public class P2PClient
	{
		IDictionary<string, WebSocket> _wsDict = new Dictionary<string, WebSocket>();

		public void Connect(string url)
		{
			if (!_wsDict.ContainsKey(url))
			{
				WebSocket ws = new WebSocket(url);
				ws.OnMessage += (sender, e) =>
				{
					if (e.Data == "Hi Client")
					{	
						Console.WriteLine(e.Data);
					}
					else
					{
						Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

						if (newChain.IsValid() && newChain.Chain.Count > Program.HandoverManuals.Chain.Count)
						{
							List<Transaction> newTransactions = new List<Transaction>();

							newTransactions.AddRange(newChain.PendingTransactions);
							newTransactions.AddRange(Program.HandoverManuals.PendingTransactions);

							newChain.PendingTransactions = newTransactions;
							Program.HandoverManuals = newChain;
						}
					}
				};

				ws.Connect();
				ws.Send("Hi Server");
				ws.Send(JsonConvert.SerializeObject(Program.HandoverManuals));

				_wsDict.Add(url, ws);
			}
		}

		public void Send(string url, string data)
		{
			foreach (var item in _wsDict)
			{
				if (item.Key == url)
				{
					item.Value.Send(data);
				}
			}
		}

		public void Broadcast(string data)
		{
			foreach (var item in _wsDict)
			{
				item.Value.Send(data);
			}
		}

		public IList<string> GetServers()
		{
			IList<string> servers = new List<string>();
			foreach (var item in _wsDict)
			{
				servers.Add(item.Key);
			}

			return servers;
		}

		public void Close()
		{
			foreach (var item in _wsDict)
			{
				item.Value.Close();
			}
		}
	}
}
