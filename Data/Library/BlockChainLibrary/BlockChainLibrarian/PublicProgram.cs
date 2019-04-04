
using AssetBlockchain;
using P2POps;

namespace BlockChainLibrarian
{
	public class PublicProgram
	{
		public static int Port = 0;
		public static Blockchain HandoverManuals = new Blockchain();
		public static string Name = "Unknown";
		public static P2PServer Server = null;
		public static P2PClient Client = new P2PClient();
	}
}
