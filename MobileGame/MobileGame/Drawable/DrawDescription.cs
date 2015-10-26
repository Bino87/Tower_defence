using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Drawable
{

	public sealed class DrawDescription
	{
		public int textureIndex;
		public Vector2 position;
		public Vector2 origin;
		public Vector2 scale;
		public SpriteEffects spriteEffect;
		public Rectangle? boundingBox;
		public Color color;
		public float rotation;
		public float depth;
		DrawDescription(int textureIndex, Vector2 position, Rectangle? boundingBox = null, Color? color = null,
						float rotation = 0.0f, Vector2 origin = default(Vector2), Vector2? scale = null,
						SpriteEffects spriteEffect = SpriteEffects.None, float depth = 1.0f)
		{
			this.textureIndex = textureIndex;
			this.position = position;
			this.boundingBox = boundingBox ?? new Rectangle(0,0,30,30);
			this.origin = origin;
			this.color = color ?? Color.White;
			this.scale = scale ?? Vector2.One;
			this.spriteEffect = spriteEffect;
			this.rotation = rotation;
			this.depth = depth;
		}




		/// <summary>
		/// Creates draw description container for the Drawable object, textureIndex and position must be asigned! others can be asigned by writing the 'parameter name': and desired value. ex: color: Color.Black
		/// </summary>
		/// <param name="textureIndex"></param>
		/// <param name="position"></param>
		/// <param name="boundingBox"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="spriteEffect"></param>
		/// <param name="depth"></param>
		/// <returns></returns>
		public static DrawDescription CreateDrawDescriptin(int textureIndex, Vector2 position, Rectangle? boundingBox = null,
														   Color? color = null,
														   float rotation = 0.0f, Vector2 origin = default(Vector2),
														   Vector2? scale = null,
														   SpriteEffects spriteEffect = SpriteEffects.None, float depth = 1.0f)
		{
			return new DrawDescription(textureIndex, position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
