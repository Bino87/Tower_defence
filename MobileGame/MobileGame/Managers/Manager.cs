using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.GameObjects;
using MobileGame.Interfaces;

namespace MobileGame.Managers
{
	class Manager
	{
		readonly ClientManager cm;
		static List <Player> players;

		public static List<Player> Players { get { return players; } set { players = value; } }


		public Manager()
		{
			cm = new ClientManager("https://blistering-heat-6102.firebaseio.com/");
			players = new List<Player>();


			for( int i = 0; i < 4; i++ )
			{
				players.Add(item : new Player(i, cm, DrawDescription.CreateDrawDescriptin(null, Vector2.One)));
			}

			cm.Client.UpdateAsync("Game", players);
		}

		public void Update(GameTime gt)
		{
			ParticleEngine.Update(gt);

			foreach( var player in players )
				player.Update(gt);

			CleanUpLists();
		}

		void CleanUpLists()
		{
			ParticleEngine.CleanUpList();
		}

		public void Draw(SpriteBatch sb)
		{
			//ParticleEngine.Draw(sb);

			//foreach(var player in players)
			//{
			//	player.Draw(sb);
			//}
		}
	}
}
