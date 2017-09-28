namespace Shizuka
{
	public class ServerData
	{
		public ulong Id { get; set; }
		public string Name { get; set; }

		public ServerData(ulong id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}