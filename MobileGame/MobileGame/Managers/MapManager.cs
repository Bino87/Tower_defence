using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.GameObjects;
using MobileGame.Interfaces;


namespace MobileGame.Managers
{

	sealed class MapManager
	{
		struct Coord
		{
			int x;
			int y;

			public int X { get { return x; } set { x = value; } }
			public int Y { get { return y; } set { y = value; } }
		}

		static int width;
		static int height;
		static ITile[,] map;
		int numberOfPlayers;
		readonly int tileSize;
		readonly int spread;


		public MapManager(int width, int height, int tileSize, int spread)
		{
			this.tileSize = tileSize;
			this.spread = spread;
			MapManager.width = width;
			MapManager.height = height;
			//map = MapGenerator.GenerateMap(width, height);
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
			Vector2 vector;
			float xOffset = width * tileSize + spread;
			foreach( var tile in map )
			{
				vector = tile.Position;
				for( int i = 0; i < numberOfPlayers; i++ )
				{
					vector.X += xOffset * i;
					tile.Position = vector;
				}

				vector.X -= xOffset * numberOfPlayers;
				tile.Position = vector;
			}
		}
	}
}
