using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobileGame.Managers;

namespace MobileGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1: Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Manager m;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

		}


		protected override void Initialize()
		{
			TextureManager.LoadTextures(Content);

			m = new Manager();
			IsMouseVisible = true;


			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.

			spriteBatch = new SpriteBatch(GraphicsDevice);

			graphics.PreferredBackBufferWidth = 1300;
			graphics.PreferredBackBufferHeight = 700;
			graphics.ApplyChanges();
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

			m.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();



			m.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
