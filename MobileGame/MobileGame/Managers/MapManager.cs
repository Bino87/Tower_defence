using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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


		public MapManager(int width, int height, int tileSize, int spread)
		{
			this.tileSize = tileSize;
			this.spread = spread;
			MapManager.width = width;
			MapManager.height = height;
			map = MapGenerator.GenerateMap(width, height);
			numberOfPlayers = 4;
		}




		public void SetNumberOfPlayers(int playerCount)
		{
			numberOfPlayers = playerCount;
		}

		public static bool IsPassable(int x, int y)
		{
			if( x < 0 || x >= width )
				return false;
			if( y < 0 || y >= height )
				return false;

			//Check if typeof(map[x,y]) is passable;

			return true;
		}

		public void Draw(SpriteBatch sb)
		{
			if(map == null)
				return;
			Vector2 vector;
			float xOffset = width * tileSize + spread;
			foreach( var tile in Map )
			{
				vector = tile.Position;
				for( int i = 0; i < numberOfPlayers; i++ )
				{
					tile.Position = vector;
					tile.Draw(sb);
					vector.X += xOffset;
				}

				vector.X -= xOffset * numberOfPlayers ;
				tile.Position = vector;
			}
		}
	}
}
