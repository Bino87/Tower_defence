
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.GameObjects.Tiles;
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

			var boolMap = new bool[width, height];
			var rand = new Random();
			var x = rand.Next(3, width - 3);
			var y = 0;
			var openList = new List<Coord>();
			var closedList = new List<Coord>();
			var currentCoord = new Coord { X = x, Y = y };
			SetBoolMapCoord(boolMap, currentCoord, true);
			closedList.Add(currentCoord);


			while( currentCoord.Y != height - 1 )
			{
				SetAdjusantCoords(width, height, currentCoord.X + 1, currentCoord.Y, closedList, openList, boolMap);
				//openList.Add(closedList[closedList.Count - 1]);
				SetAdjusantCoords(width, height, currentCoord.X - 1, currentCoord.Y, closedList, openList, boolMap);
				//openList.Add(closedList[closedList.Count - 1]);
				if( SetAdjusantCoords(width, height, currentCoord.X, currentCoord.Y + 1, closedList, openList, boolMap) )
				{
					//openList.Add(closedList[closedList.Count - 1]);
					AddToOpenList(openList, currentCoord, width);
					//openList.Add(new Coord { X = currentCoord.X, Y = currentCoord.Y+1+1 });
					SetAdjusantCoords(width, height, currentCoord.X, currentCoord.Y, closedList, openList, boolMap);
				}

				openList = openList.FindAll(coord => coord.X > 1 && coord.X < width - 2);
				var index = rand.Next(openList.Count);
				currentCoord = openList[index];
				openList.Clear();


			}
			if( SetAdjusantCoords(width, height, currentCoord.X, currentCoord.Y, closedList, openList, boolMap) )
				openList.Add(closedList[closedList.Count - 1]);
			if( SetAdjusantCoords(width, height, currentCoord.X + 1, currentCoord.Y, closedList, openList, boolMap) )
				openList.Add(closedList[closedList.Count - 1]);
			if( SetAdjusantCoords(width, height, currentCoord.X - 1, currentCoord.Y, closedList, openList, boolMap) )
				openList.Add(closedList[closedList.Count - 1]);
			//Debug Do not remove!
			//WriteDebug(boolMap);
			openList = null;
			closedList = null;

			return PopulateTileMap(width, height, boolMap);
		}

		static void AddToOpenList(List<Coord> openList, Coord currentCoord, int width)
		{
			if( currentCoord.X +1 < width-1 )
				openList.Add(new Coord { X = currentCoord.X + 1, Y = currentCoord.Y + 1 });
			if( currentCoord.X > 0 )
				openList.Add(new Coord { X = currentCoord.X - 1, Y = currentCoord.Y + 1 });
		}

		static void SetBoolMapCoord(bool[,] map, Coord coord, bool boolToSet)
		{
			map[coord.X, coord.Y] = boolToSet;
		}

		static ITile[,] PopulateTileMap(int width, int height, bool[,] boolMap)
		{
			var map = new ITile[width, height];

			for( int x = 0; x < width; x++ )
			{
				for( int y = 0; y < height; y++ )
				{
					map[x, y] = boolMap[x, y]
						? (ITile)
						new EnemyPath(RenderDesc.CreateDrawDescriptin(
							TextureManager.GetTextureIndex(typeof(EnemyPath)),
							new Vector2(x * 30, y * 30))) 
							:
						new Ground(RenderDesc.CreateDrawDescriptin(
							TextureManager.GetTextureIndex(typeof(Ground)),
							new Vector2(x * 30, y * 30)));
				}
			}
			return map;
		}

		static bool SetAdjusantCoords(int width, int height, int x, int y, ICollection<Coord> closedList, ICollection<Coord> openList, bool[,] boolMap)
		{
			if( x < 1 || x >= width - 1 )
				return true;
			if( y < 0 || y >= height )
				return true;

			var coord = new Coord { X = x, Y = y };

			if( closedList.Contains(coord) )
				return true;
			if( openList.Contains(coord) )
				return true;

			closedList.Add(coord);
			SetBoolMapCoord(boolMap, coord, true);
			return true;
		}

		static void WriteDebug(bool[,] map)
		{
			Stream s;
			try
			{
				s = File.Create("C:\\Users\\Kamil\\Desktop\\debug.txt");
			} catch( Exception )
			{
				return;
			}
			StreamWriter sw = new StreamWriter(s);


			for( int y = 0; y < map.GetLength(1); y++ )
			{
				string str = string.Empty;
				for( int x = 0; x < map.GetLength(0); x++ )
				{
					str = string.Format("{0}{1}", str, map[x, y] ? "X" : "|");
				}
				sw.WriteLine(str);
			}

			sw.Close();
		}
	}
}
