using FireSharp;
using FireSharp.Config;
using FireSharp.EventStreaming;
using FireSharp.Interfaces;
using Microsoft.Xna.Framework;
using MobileGame.Enums;
using MobileGame.GameObjects.Player;

namespace MobileGame.Managers
{

	public class ClientManager 
	{
		IFirebaseClient client;
		public ClientManager(string basepath, string authSecret = null)
		{
			IFirebaseConfig config = new FirebaseConfig
									 {
										 BasePath = basepath
									 };
			if( authSecret != null )
				config.AuthSecret = authSecret;
			client = new FirebaseClient(config);
			Listen();
		}


		public IFirebaseClient Client { get { return client; } set { client = value; } }


		async void Listen()
		{
			//await client.OnAsync("", OnValueAdded, OnValueChanged, OnValueRemoved);
		}


		void OnValueRemoved(object sender, ValueRemovedEventArgs args)
		{

		}


		void OnValueAdded(object sender, ValueAddedEventArgs args)
		{

		}


		void OnValueChanged(object sender, ValueChangedEventArgs args)
		{

			DecodePath(args.Path, args.Data);
		}

		void DecodePath(string path, string data)
		{
			path = path.Substring(1);
			var folders = path.Split('/');
			int index = 0;

			switch( folders[index] )
			{
			case "Game":
				GetPlayerIndex(folders, index+1, data);
				break;
			case "Lobby":
				break;
			}
		}


		void GetPlayerIndex(string[] folders, int index, string data)
		{

			switch( folders[index] )
			{
			case "0":
				GetPlayersVariable(folders, index+1, data, Manager.Players[0]);
				break;
			case "1":
				GetPlayersVariable(folders, index+1, data, Manager.Players[1]);
				break;
			case "2":
				GetPlayersVariable(folders, index+1, data, Manager.Players[2]);
				break;
			case "3":
				GetPlayersVariable(folders, index+1, data, Manager.Players[3]);
				break;
			}
		}


		void GetPlayersVariable(string[] folders, int index, string data, Player player)
		{
			switch( folders[index] )
			{
			case "Gold":
				player.Gold = int.Parse(data);
				break;
			case "Kills": break;
			case "LivesLeft": break;
			case "Position":

				var split = data.Split(',');
				var tempVector = new Vector2
				{
					X = float.Parse(split[0]),
					Y = float.Parse(split[1])
				};
				player.Position = tempVector;


				break;
			case "Score": break;
			case "Status":
				int enumId = int.Parse(data);
				player.Status = (PlayerStatus)enumId;
				break;
			}
		}

	}
}
