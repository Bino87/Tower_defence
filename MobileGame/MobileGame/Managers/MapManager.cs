using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.Interfaces;

namespace MobileGame.Managers
{

	sealed class MapManager
	{
		static int width;
		static int height;
		static ITile[,] map;
		int numberOfPlayers;
		readonly int tileSize;
		readonly int spread;
		public static ITile[,] Map { get { return map; } set { map = value; } }


		public MapManager(int width, int height, int tileSize, int spread, int playersCount)
		{
			this.tileSize = tileSize;
			this.spread = spread;
			MapManager.width = width;
			MapManager.height = height;
			map = MapGenerator.GenerateMap(width, height);
			numberOfPlayers = playersCount;
		}


		public void SetNumberOfPlayers(int playerCount)
		{
			numberOfPlayers = playerCount;
		}

		public static bool IsPassable(int x, int y)
		{
			if( map == null )
				return false;
			if( x < 0 || x >= width )
				return false;
			if( y < 0 || y >= height )
				return false;

			return map[x, y].IsPassable();
		}

		public void Draw(SpriteBatch sb)
		{
			if( map == null )
				return;
			var test = new List <ITile>();
			int y = 0;
			int x = 1;

			while(y < height )
			{
				for( ; x <  width -1; x++)
				{
					if(!IsPassable(x, y))
						continue;
					if(!IsPassable(x - 1, y) || !IsPassable(x + 1, y))
						continue;
					test.Add(map[x, y]);
					y++;
					x = x - 1;
					break;
				}
			}

			foreach(var tile in test)
			{
				tile.Draw(sb);
			}

			if(Keyboard.GetState().IsKeyDown(Keys.Space))
				return;
			float xOffset = width * tileSize + spread;
			foreach( var tile in Map )
			{
				var vector = tile.Position;
				for( var i = 0; i < numberOfPlayers; i++ )
				{
					tile.Position = vector;
					tile.Draw(sb);
					vector.X += xOffset;
				}

				vector.X -= xOffset * numberOfPlayers;
				tile.Position = vector;
			}
		}
	}
}
