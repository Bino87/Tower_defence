
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.GameObjects.Tiles;
using MobileGame.Interfaces;
using MobileGame.Structs;

namespace MobileGame.Managers
{
	class MapGenerator
	{

		public static MapDescription GenerateMap(int width, int height)
		{

			MapDescription mapDesc = new MapDescription
			{
				Map = new ITile[width, height],
				Path = new Queue<Vector2>()
			};
			var boolMap = new bool[width, height];
			var rand = new Random();
			var x = rand.Next(3, width - 3);
			var y = 0;
			var openList = new List<Coord>();
			var closedList = new List<Coord>();
			Coord currentCoord = new Coord { X = x, Y = y };
			SetBoolMapCoord(boolMap, currentCoord, true);
			closedList.Add(currentCoord);
			mapDesc.EnQueue(currentCoord);


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
				mapDesc.EnQueue(currentCoord);
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

			return PopulateTileMap(width, height, boolMap, mapDesc);
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

		static MapDescription PopulateTileMap(int width, int height, bool[,] boolMap, MapDescription mapDesc)
		{
			var map = new ITile[width, height];
			Vector2 offset = new Vector2(MapManager.TileSize /2.0f);
			Vector2 vec;
			for( int x = 0; x < width; x++ )
			{
				for( int y = 0; y < height; y++ )
				{
					vec = new Vector2(x * 30, y * 30) + offset;
					map[x, y] = boolMap[x, y]
						? (ITile)
						new EnemyPath(RenderDesc.CreateDrawDescriptin<EnemyPath>(vec)) 
							:
						new Ground(RenderDesc.CreateDrawDescriptin<Ground>(vec));
				}
			}
			mapDesc.Map = map;
			return mapDesc;
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
