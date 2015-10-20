using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.GameObjects
{
	class Drawable
	{
		readonly Texture2D texture;
		readonly Vector2 position;
		readonly Vector2 origin;
		readonly Vector2 scale;
		readonly SpriteEffects spriteEffect;
		readonly Rectangle? boundingBox;
		readonly Color color;
		readonly float rotation;
		readonly float depth;


		public Drawable(DrawDescription drawDescription)
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
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, position, boundingBox, color, rotation, origin, scale, spriteEffect, depth);
		}
	}
}
