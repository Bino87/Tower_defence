using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public abstract class GameObject : Renderable, IGameObject
	{
		protected GameObject(RenderDesc renderDesc)
			: base(renderDesc)
		{
			
		}

		public abstract void Update(GameTime gt);
		public bool IsAlive { get { return isAlive; } }
	}
}
