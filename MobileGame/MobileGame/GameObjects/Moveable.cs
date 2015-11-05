using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Moveable: Renderable, IGameObject, IMoveable
	{
		protected Vector2 direction;
		readonly float speed;
		public Moveable(Vector2 destination, float speed, RenderDesc renderDesc)
			: base(renderDesc)
		{
			this.speed = speed;
			direction = destination - position;
			direction.Normalize();
		}




		public virtual void Update(GameTime gt)
		{
			position += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}
