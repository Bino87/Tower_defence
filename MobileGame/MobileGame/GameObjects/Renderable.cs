using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Description;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Renderable: IRenderable
	{
		readonly int textureIndex;
		protected Vector2 position;
		readonly Vector2 origin;
		protected Vector2 scale;
		readonly SpriteEffects spriteEffect;
		readonly Rectangle boundingBox;
		readonly Color color;
		protected float rotation;
		readonly float depth;
		protected bool isAlive;
		public bool IsAlive { get { return isAlive; } }

		public Vector2 Position { get { return position; } set { position = value; } }


		protected Renderable(RenderDesc renderDesc)
		{
			textureIndex =	renderDesc.TextureIndex;
			position =		renderDesc.Position;
			boundingBox =	renderDesc.BoundingBox;
			origin =		renderDesc.Origin;
			color =			renderDesc.Color;
			scale =			renderDesc.Scale;
			spriteEffect =	renderDesc.SpriteEffect;
			rotation =		renderDesc.Rotation;
			depth =			renderDesc.Depth;
			isAlive = true;
		}

		public virtual void Draw(SpriteBatch sb)
		{
			if( isAlive )
				sb.Draw(TextureManager.GetTexture(textureIndex), position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
