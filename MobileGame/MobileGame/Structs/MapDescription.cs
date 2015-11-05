using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.Structs
{
	struct MapDescription
	{
		public ITile[,] Map { get; set; }
		public Queue<Vector2> Path { get; set; }


		public void EnQueue(Coord coord)
		{
			Vector2 vec = new Vector2(coord.X, coord.Y) * MapManager.TileSize;
			Vector2 offset = new Vector2(MapManager.TileSize /2.0f);
			Path.Enqueue(vec + offset);
		}
	}
}
