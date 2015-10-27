using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Drawable;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Renderable: IRenderable
	{
		readonly int textureIndex;
		protected Vector2 position;
		readonly Vector2 origin;
		readonly Vector2 scale;
		readonly SpriteEffects spriteEffect;
		readonly Rectangle boundingBox;
		readonly Color color;
		readonly float rotation;
		readonly float depth;
		protected bool isAlive;

		public Vector2 Position { get { return position; } set { position = value; } }


		protected Renderable(RenderDesc renderDesc)
		{
			textureIndex =	renderDesc.textureIndex;
			position =		renderDesc.position;
			boundingBox =	renderDesc.boundingBox;
			origin =		renderDesc.origin;
			color =			renderDesc.color;
			scale =			renderDesc.scale;
			spriteEffect =	renderDesc.spriteEffect;
			rotation =		renderDesc.rotation;
			depth =			renderDesc.depth;
			isAlive = true;
		}

		public virtual  void Draw(SpriteBatch sb)
		{
			if( isAlive )
				sb.Draw(TextureManager.GetTexture(textureIndex), position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
