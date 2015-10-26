using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public abstract class GameObject : Drawable, IGameObject
	{
		protected GameObject(DrawDescription drawDescription)
			: base(drawDescription)
		{
			
		}

		public abstract void Update(GameTime gt);
		public bool IsAlive { get { return isAlive; } }
	}
}
