using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MobileGameServer
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1: Game
	{
		GraphicsDeviceManager graphics;
		Texture2D path, grass;
		bool[,] map;
		SpriteBatch spriteBatch;
		Random rand;


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";


		}


		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}


		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			graphics.PreferredBackBufferWidth = 1215;
			graphics.PreferredBackBufferHeight = 700;
			graphics.ApplyChanges();
			
			rand = new Random();

			path = new Texture2D(GraphicsDevice, 30, 30);
			Color[] a = new Color[30*30];
			for( int i = 0; i < a.Length; i++ )
			{
				a[i] = Color.SaddleBrown;
			}
			path.SetData(a);

			grass = Content.Load<Texture2D>("grass");
			GenerateMap();
			// TODO: use this.Content to load your game content here
		}


		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if( Keyboard.GetState().IsKeyDown(Keys.Escape) )
				Exit();
			if( Keyboard.GetState().IsKeyDown(Keys.LeftControl) )
			{
				GenerateMap();
			}



			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			for( int x = 0; x < map.GetLength(0); x++ )
			{
				for( int y = 0; y < map.GetLength(1); y++ )
				{
					Texture2D t = map[x, y] ? path : grass;
					Vector2 temp = new Vector2(x*t.Width, y*t.Height);
					for( int i = 0; i < 4; i++ )
					{
						spriteBatch.Draw(t, temp, Color.White);
						temp.X +=  300 + 15;
					}

				}
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}


		void GenerateMap()
		{
		Generate:
			map = new bool[10, 20];
			var blocked = new List<Coord>();
			var openList = new List<Coord>();
			var currentCoord = new Coord { X= rand.Next(1, map.GetLength(0)-1), Y = 0 };
			SetMap(currentCoord);
			blocked.Add(currentCoord);
			//currentCoord.Y++;

			while( currentCoord.Y < map.GetLength(1) -1 )
			{
				for( int x = -1; x <= 1; x++ )
				{
					for( int y = -1; y <= 1; y++ )
					{
						if( Math.Abs(x) == Math.Abs(y) )
							continue;
						var tempCoord = new Coord { X = currentCoord.X + x, Y = currentCoord.Y + y };

						if( tempCoord.X < 1 || tempCoord.X >= map.GetLength(0)-1 )
							continue;
						if( tempCoord.Y < 1 || tempCoord.Y >= map.GetLength(1) )
							continue;


						if( blocked.Contains(tempCoord) )
							continue;
						openList.Add(tempCoord);

					}
				}

				if( openList.Count == 0 )
					break;
				int random = rand.Next(openList.Count);
				currentCoord = openList[random];
				SetMap(currentCoord);

				foreach( var m in openList )
				{
					blocked.Add(m);
				}

				openList.Clear();
			}

			if( currentCoord.Y != map.GetLength(1) - 1 )
			{
				goto Generate;
			}
		}


		bool CanIPlaceHere(Coord coord)
		{
			int a = 0;
			for( int x = -1; x <= 1; x++ )
			{
				for( int y = -1; y <= 1; y++ )
				{
					a++;
					if( y == 0 || x == 0 )
						continue;
					if( coord.Y +y == map.GetLength(1) )
						continue;

					if( map[coord.X + x, coord.Y + y] )
						return true;
				}
			}

			a = a;
			return false;
		}

		void SetMap(Coord coord)
		{
			map[coord.X, coord.Y] = !map[coord.X, coord.Y];
		}

		struct Coord
		{
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}
