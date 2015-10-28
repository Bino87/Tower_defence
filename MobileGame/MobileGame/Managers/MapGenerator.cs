
using System;
using System.Collections.Generic;
using MobileGame.Interfaces;

namespace MobileGame.Managers
{
	class MapGenerator
	{
		struct Coord
		{
			int x;
			int y;

			public int X { get { return x; } set { x = value; } }
			public int Y { get { return y; } set { y = value; } }
		}

		public static ITile[,] GenerateMap(int width, int height)
		{
			int x = 0;
			int y = 0;
			int index = 0;
			Coord currentCoord;
			var openList = new List<Coord>();
			var closedList = new List<Coord>();
			bool[,] boolMap = new bool[width, height];
			Random rand = new Random(boolMap.GetHashCode());


			x = rand.Next(2, width - 2);
			currentCoord = new Coord { X = x, Y = y };
			closedList.Add(currentCoord);

			SetAdjusantCoords(width, height, currentCoord.X + 1, 0, closedList, openList, boolMap);
			SetAdjusantCoords(width, height, currentCoord.X - 1, 0, closedList, openList, boolMap);
			SetAdjusantCoords(width, height, 0, currentCoord.Y + 1, closedList, openList, boolMap);
			SetAdjusantCoords(width, height, 0, currentCoord.Y - 1, closedList, openList, boolMap);

			foreach(var coord in openList)
			{
				
			}

			while(openList.Count != 0)
			{
				
			}

			return null;
		}


		static void SetAdjusantCoords(int width, int height, int x, int y, List<Coord> closedList, List<Coord> openList, bool[,] boolMap)
		{
			if( x < 0 || x >= width )
				return;
			if( y < 0 || y >= height )
				return;

			var coord = new Coord { X = x, Y = y };

			if( closedList.Contains(coord) )
				return;
			if( openList.Contains(coord) )
				return;

			openList.Add(coord);
			boolMap[coord.X, coord.Y] = true;
		}


		static void Test(bool[,] map)
		{
			map[0, 0] = true;
		}
	}
}
