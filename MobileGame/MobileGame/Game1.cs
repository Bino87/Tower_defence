using FireSharp;
using FireSharp.EventStreaming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.GameObject;

namespace MobileGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1: Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		FirebaseClient client;
		Texture2D tex;
		Vector2 position;
		Color color;

		object key = new object();
			Drawable d;



		Vector2 Position
		{
			get
			{
				lock( key )
				{
					return position;
				}
			}

			set
			{
				lock( key )
				{
					position = value;
				}
			}
		}


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			//IFirebaseConfig config = new FirebaseConfig
			//{
			//	BasePath = "https://blistering-heat-6102.firebaseio.com/"
			//};


			//client = new FirebaseClient(config);
			//client.Delete("");
			//position = new Vector2(100);

			//var setResponse = client.SetAsync("Position", config);


			//new Thread(StringToVector).Start();

			//Lol();


			tex = Content.Load<Texture2D>("sdfsf");
			d = new Drawable(DrawDescription.CreateDrawDescriptin(tex,position, rotation: .2f));
			
			//tex = LoadTexture2D("sdfsf.png");

			base.Initialize();
		}


		async void Lol()
		{

		var	esr = await client.OnAsync("Position", OnValueAdded, OnValueChanged, OnValueRemoved);

		}


		void OnValueRemoved(object sender, ValueRemovedEventArgs args)
		{
			var a = 10;
		}


		void OnValueAdded(object sender, ValueAddedEventArgs args)
		{
			int a = 10;
		}


		int i;
		void OnValueChanged(object sender, ValueChangedEventArgs args)
		{
			color.R = (byte)((color.R + 1) % byte.MaxValue);
			color.G = (byte)((color.G + 1) % byte.MaxValue);
			color.B = (byte)((color.B + 1) % byte.MaxValue);
			i++;

		}





		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}


		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}


		protected override void Update(GameTime gameTime)
		{
			if( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) )
				Exit();



			base.Update(gameTime);
		}


		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();


			spriteBatch.Draw(tex, Position, color);
			d.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
