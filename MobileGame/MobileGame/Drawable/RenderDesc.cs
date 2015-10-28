using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Drawable
{

	public sealed class RenderDesc
	{
		int textureIndex;
		Vector2 position;
		Vector2 origin;
		Vector2 scale;
		SpriteEffects spriteEffect;
		Rectangle boundingBox;
		Color color;
		float rotation;
		float depth;




		RenderDesc(int textureIndex, Vector2 position, Rectangle? boundingBox = null, Color? color = null,
						float rotation = 0.0f, Vector2 origin = default(Vector2), Vector2? scale = null,
						SpriteEffects spriteEffect = SpriteEffects.None, float depth = 1.0f)
		{
			this.textureIndex = textureIndex;
			this.position = position;
			this.boundingBox = boundingBox ?? new Rectangle(0, 0, 30, 30);
			this.origin = new Vector2((float)this.boundingBox.Width/2, (float)this.boundingBox.Height/2);
			this.color = color ?? Color.White;
			this.scale = scale ?? Vector2.One;
			this.spriteEffect = spriteEffect;
			this.rotation = rotation;
			this.depth = depth;
		}


		public int TextureIndex { get { return textureIndex; } set { textureIndex = value; } }
		public Vector2 Position { get { return position; } set { position = value; } }
		public float Depth { get { return depth; } set { depth = value; } }
		public float Rotation { get { return rotation; } set { rotation = value; } }
		public Color Color { get { return color; } set { color = value; } }
		public Rectangle BoundingBox { get { return boundingBox; } set { boundingBox = value; } }
		public SpriteEffects SpriteEffect { get { return spriteEffect; } set { spriteEffect = value; } }
		public Vector2 Scale { get { return scale; } set { scale = value; } }
		public Vector2 Origin { get { return origin; } set { origin = value; } }


		/// <summary>
		/// Creates draw description container for the Renderable object, textureIndex and position must be asigned! others can be asigned by writing the 'parameter name': and desired value. ex: color: Color.Black
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
		public static RenderDesc CreateDrawDescriptin(int textureIndex, Vector2 position, Rectangle? boundingBox = null,
														   Color? color = null,
														   float rotation = 0.0f, Vector2 origin = default(Vector2),
														   Vector2? scale = null,
														   SpriteEffects spriteEffect = SpriteEffects.None, float depth = 1.0f)
		{
			return new RenderDesc(textureIndex, position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
