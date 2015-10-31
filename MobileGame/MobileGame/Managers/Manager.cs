using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.Drawable;
using MobileGame.GameObjects.Player;

namespace MobileGame.Managers
{
	class Manager
	{
		readonly MapManager map;
		static List <Player> players;

		public static List<Player> Players { get { return players; } set { players = value; } }


		public Manager()
		{
			var cm = new ClientManager("https://blistering-heat-6102.firebaseio.com/");
			Players = new List<Player>();

			for( var i = 0; i < 4; i++ )
			{
				Players.Add(new Player(i, cm, RenderDesc.CreateDrawDescriptin(TextureManager.GetTextureIndex(typeof(Player)), Vector2.One)));
			}

			map = new MapManager(10, 20, 30, 30, players.Count);

			cm.Client.UpdateAsync("Game", Players);
		}

		public void Update(GameTime gt)
		{
			ParticleEngine.Update(gt);

			foreach( var player in Players )
				player.Update(gt);

			if( Keyboard.GetState().IsKeyDown(Keys.P) )
				MapManager.Map = MapGenerator.GenerateMap(10, 20);

			ParticleEngine.CleanUpList();
		}

		public void Draw(SpriteBatch sb)
		{

			map.Draw(sb);

			ParticleEngine.Draw(sb);

			foreach( var player in Players )
			{
				player.Draw(sb);
			}
		}
	}
}
