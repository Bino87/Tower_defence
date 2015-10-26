using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Drawable;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Drawable : ICanDraw
	{
		readonly Type texture;
		protected Vector2 position;
		readonly Vector2 origin;
		readonly Vector2 scale;
		readonly SpriteEffects spriteEffect;
		readonly Rectangle? boundingBox;
		readonly Color color;
		readonly float rotation;
		readonly float depth;
		protected bool isAlive;


		protected Drawable(DrawDescription drawDescription)
		{
			texture =		drawDescription.texture;
			position =		drawDescription.position;
			boundingBox =	drawDescription.boundingBox;
			origin =		drawDescription.origin;
			color =			drawDescription.color;
			scale =			drawDescription.scale;
			spriteEffect =	drawDescription.spriteEffect;
			rotation =		drawDescription.rotation;
			depth =			drawDescription.depth;
			isAlive = true;
		}

		public void Draw(SpriteBatch sb)
		{
			if( isAlive )
				sb.Draw(TextureManager.GetTexture(texture), position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
