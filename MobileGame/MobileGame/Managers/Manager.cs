using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Description;
using MobileGame.Extensions;
using MobileGame.GameObjects.Player;

namespace MobileGame.Managers
{
	class Manager
	{
		static List <Player> players;

		public static List<Player> Players { get { return players; } set { players = value; } }


		public Manager()
		{
			//var cm = new ClientManager("https://blistering-heat-6102.firebaseio.com/");
			var cm = new ClientManager("https://vivid-heat-894.firebaseio.com/");
		
			Players = new List<Player>();
			//cm.Client.DeleteAsync("");
			var pCount = 4;
			MapManager.GenerateMap(10, 20, 30, 30, pCount);
			for( var i = 0; i < pCount; i++ )
			{
				Players.Add(new Player(i, cm, RenderDesc.CreateDrawDescriptin<Player>(Vector2.One,origin: Vector2.Zero)));

			}

			//cm.Client.UpdateAsync("Game", Players);
		}

		public void Update(GameTime gt)
		{
			ParticleEngine.Update(gt);

			foreach( var player in players )
				player.Update(gt);

			if(SpawnManager.SpawnEnemy(gt))
				foreach(var player in players)
					player.SpawnEnemy(player.GetRandomEnemyType());

			ParticleEngine.CleanUpList();
		}

		public void Draw(SpriteBatch sb)
		{

			MapManager.Draw(sb);

			ParticleEngine.Draw(sb);
			

			foreach( var player in Players )
			{
				player.Draw(sb);
			}
		}
	}
}
