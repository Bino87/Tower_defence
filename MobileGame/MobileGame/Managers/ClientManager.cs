using System;
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

		public IFirebaseClient Client { get { return client; } set { client = value; } }


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



		async void Listen()
		{
			await client.OnAsync("", OnValueAdded, OnValueChanged);
		}

		void OnValueAdded(object sender, ValueAddedEventArgs args)
		{
			DecodePath(args.Path, args.Data, String.Empty);
		}

		void OnValueChanged(object sender, ValueChangedEventArgs args)
		{
			DecodePath(args.Path, args.Data, args.OldData);
		}

		void DecodePath(string path, string data, string oldData)
		{
			path = path.Substring(1);
			var folders = path.Split('/');
			int index = 0;

			switch( folders[index] )
			{
			case "Game":
				GetPlayerIndex(folders, data, ++index);
				break;
			case "Lobby":
				OnLobbyChange(folders, data, oldData);
				break;
			case "Settings":
				OnSettingsChange(folders, data, oldData, ++index);
				break;
			case "GameState":
				OnGameStateChange(folders, data, oldData, ++index);
				break;
			}
		}

		void OnGameStateChange(string[] folders, string data, string oldData, int i)
		{
		}

		void OnSettingsChange(string[] folders, string data, string oldData, int index)
		{

		}

		void OnLobbyChange(string[] folders, string data, string oldData)
		{

		}

		void GetPlayerIndex(string[] folders, string data, int index)
		{
			if( folders.Length < 3 )
				return;
			if( folders[folders.Length - 1].Equals("Position") || folders[folders.Length - 1].Equals("Status") )
			{
				int pIndex;
				if( !int.TryParse(folders[index], out pIndex) )
					return;
				++index;
				GetPlayersVariable(folders, data, Manager.Players[pIndex], index);
			}
		}

		void GetPlayersVariable(string[] folders, string data, Player player, int index)
		{
			switch( folders[index] )
			{
			case "Position":

				var split = data.Split(',');
				var tempVector = new Vector2
				{
					X = float.Parse(split[0]),
					Y = float.Parse(split[1])
				};
				player.Position = tempVector;


				break;

			case "Status":
				int intData;
				if( !int.TryParse(data, out intData) )
					break;
				if( intData == 0 )
				{
					player.Status = PlayerStatus.Idle;
					break;
				}

				if( player.Status == PlayerStatus.Idle )
					player.BuildTower((PlayerStatus)intData);
				else
					player.Status = (PlayerStatus)intData;

				break;
			}
		}
	}
}
