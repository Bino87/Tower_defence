using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{
	abstract class Particle: Moveable, IParticle
	{
		float lifeTime;


		protected Particle(float lifeTime, float speed, Vector2 direction, RenderDesc renderDesc)
			: base(direction, speed, renderDesc)
		{
			this.lifeTime = lifeTime;
		}

		public override void Update(GameTime gt)
		{
			if( !isAlive )
				return;
			base.Update(gt);
			lifeTime -= (float)gt.ElapsedGameTime.TotalSeconds;
			if( lifeTime <= 0.0f )
				isAlive = false;
		}
	}
}
