using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.Drawable;
using MobileGame.GameObjects;

namespace MobileGame.Managers
{
	class Manager
	{
		readonly ClientManager cm;
		static List <Player> players;

		public static List<Player> Players { get { return players; } set { players = value; } }
		MouseState ms, oms;


		public Manager()
		{
			ms = Mouse.GetState();
			oms = ms;
			cm = new ClientManager("https://blistering-heat-6102.firebaseio.com/");
			players = new List<Player>();
			MapManager map = new MapManager(10, 20, 30, 30);

			for( int i = 0; i < 2; i++ )
			{
				players.Add(new Player(i, cm, RenderDesc.CreateDrawDescriptin(TextureManager.GetTextureIndex(typeof(Player)), Vector2.One)));
			}

			cm.Client.UpdateAsync("Game", players);
		}

		public void Update(GameTime gt)
		{
			Fidle();

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
			ParticleEngine.Draw(sb);

			foreach( var player in players )
			{
				player.Draw(sb);
			}
		}
	}
}
