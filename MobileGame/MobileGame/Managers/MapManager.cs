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
		Random rand;


		public MapManager(int width, int height, int tileSize, int spread)
		{
			this.tileSize = tileSize;
			this.spread = spread;
			rand = new Random(GetHashCode());
			MapManager.width = width;
			MapManager.height = height;
			map = new ITile[width, height];
			GenerateMap();
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

			//Check if tile at coords x and y is passable;

			return true;
		}

		void GenerateMap()
		{

			int x = 0;
			int y = 0;
			int index = 0;
			Coord currentCoord;
			var openList = new List<Coord>();
			var closedList = new List<Coord>();
			bool[,] boolMap = new bool[width, height];

			x = rand.Next(2, width - 2);
			currentCoord = new Coord { X = x, Y = y };
			boolMap[x, y] = true;
			closedList.Add(currentCoord);

			for( int xCoord = -1; xCoord < 2; xCoord++ )
			{
				for( int yCoord = -1; yCoord < 2; yCoord++ )
				{
					if( Math.Abs(xCoord) == Math.Abs(yCoord) )
						continue;
					if( x + xCoord < 0 || x + xCoord >= width )
						continue;
					if( y + yCoord < 0 || y + yCoord >= height )
						continue;

					Coord coord = new Coord { X = x + xCoord, Y = y + yCoord };
					closedList.Add(coord);
					boolMap[coord.X, coord.Y] = true;
				}
			}

			index = rand.Next(1, closedList.Count);
			currentCoord = closedList[index];
			closedList.Remove(currentCoord);
			openList.Add(currentCoord);

			while( currentCoord.Y != height )
			{
				openList.Clear();

				for( int xCoord = -1; xCoord < 2; xCoord++ )
				{
					for( int yCoord = -1; yCoord < 2; yCoord++ )
					{
						if( Math.Abs(xCoord) == Math.Abs(yCoord) )
							continue;
						if( currentCoord.X + xCoord < 0 || currentCoord.X + xCoord >= width )
							continue;
						if( currentCoord.Y + yCoord < 0 || currentCoord.Y + yCoord >= height )
							continue;

						Coord coord = new Coord { X = x + xCoord, Y = y + yCoord };
						if(openList.Contains(coord))
							continue;
						if(closedList.Contains(coord))
							continue;
						openList.Add(coord);
					}
				}

				//closedList.AddRange(openList);
				index = rand.Next(openList.Count);
				currentCoord = openList[index];
				openList.Clear();

				for( int xCoord = -1; xCoord < 2; xCoord++ )
				{
					for( int yCoord = -1; yCoord < 2; yCoord++ )
					{
						if( Math.Abs(xCoord) == Math.Abs(yCoord) )
							continue;
						if( currentCoord.X + xCoord < 0 || currentCoord.X + xCoord >= width )
							continue;
						if( currentCoord.Y + yCoord < 0 || currentCoord.Y + yCoord >= height )
							continue;

						Coord coord = new Coord { X = x + xCoord, Y = y + yCoord };
						if( openList.Contains(coord) )
							continue;
						if( closedList.Contains(coord) )
							continue;
						openList.Add(coord);
						boolMap[coord.X, coord.Y] = true;
					}
				}

				closedList.AddRange(openList);
				index = rand.Next(openList.Count);
				currentCoord = openList[index];
				openList.Clear();
			}


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
