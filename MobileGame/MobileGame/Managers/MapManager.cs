using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.Interfaces;
using MobileGame.Structs;

namespace MobileGame.Managers
{

	public static class MapManager
	{
		static int width;
		static int height;
		static ITile[,] map;
		static int numberOfPlayers;
		static int tileSize;
		static int spread;
		public static ITile[,] Map { get { return map; } set { map = value; } }
		public static int TileSize { get { return tileSize; } }
		public static int Spread { get { return spread; } }


		public static void GenerateMap(int desiredMapWidth, int desiredMapHeight, int mapTileSize, int mapSpread, int playersCount)
		{
			tileSize = mapTileSize;
			spread = mapSpread;
			width = desiredMapWidth;
			height = desiredMapHeight;
			map = MapGenerator.GenerateMap(desiredMapWidth, desiredMapHeight);
			numberOfPlayers = playersCount;
		}

		public static bool IsPassable(Coord coord)
		{
			int x = coord.X;
			int y = coord.Y;
			if( map == null )
				return false;
			if( x < 0 || x >= width )
				return false;
			if( y < 0 || y >= height )
				return false;

			return map[x, y].CanPlaceTower();
		}

		static bool IsPassable(int x, int y)
		{
			if( map == null )
				return false;
			if( x < 0 || x >= width )
				return false;
			if( y < 0 || y >= height )
				return false;

			return map[x, y].CanPlaceTower();
		}

		public static void Draw(SpriteBatch sb)
		{
			if( map == null )
				return;
			//var test = new List<ITile>();
			//int y = 0;
			//int x = 1;

			//while( y < height )
			//{
			//	for( ; x <  width -1; x++ )
			//	{
			//		if( !IsPassable(x, y) )
			//			continue;
			//		if( !IsPassable(x - 1, y) || !IsPassable(x + 1, y) )
			//			continue;
			//		test.Add(map[x, y]);
			//		y++;
			//		x = x - 1;
			//		break;
			//	}
			//}

			//foreach( var tile in test )
			//{
			//	tile.Draw(sb);
			//}

			if( Keyboard.GetState().IsKeyDown(Keys.Space) )
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

		public static float GetXaxisOffser(int index)
		{
			float indexOffset = tileSize * width * index;
			float indexSpread = spread * index;

			return indexSpread + indexOffset;
		}


		public static Coord GetTileIndexFromPosition(Vector2 position, int index)
		{
			int x = (int)(position.X - GetXaxisOffser(index));
			int y = (int)(position.Y);
			x -= (x % tileSize);
			y -= y % tileSize;
			x /= tileSize;
			y /= tileSize;

			if(x < 0)
				x = 0;
			if(x >= width)
				x = width - 1;
			if(y < 0)
				y = 0;
			if(y >= height)
				y = height - 1;
			var coord = new Coord { X = x, Y =y };

			return coord;
		}
	}
}
