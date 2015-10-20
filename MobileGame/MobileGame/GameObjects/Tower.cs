using Microsoft.Xna.Framework;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	abstract class Tower : Drawable, ITower
	{
		protected Tower(DrawDescription drawDescription)
			: base(drawDescription)
		{
			
		}

		public abstract void Update(GameTime gameTime);
		public abstract void Shoot();
	}

}
