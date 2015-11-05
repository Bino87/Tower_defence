using Microsoft.Xna.Framework;
using MobileGame.Description;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public  class Particle: Moveable, IParticle
	{
		float lifeTime;
		readonly float scaleFactor;

		public Particle(float lifeTime, float speed, Vector2 direction, RenderDesc renderDesc)
			: base(direction, speed, renderDesc)
		{
			this.lifeTime = lifeTime;
			this.direction = direction;
			scale.X = .1f;
			scale.Y = .1f;
			scaleFactor = 1f / lifeTime;
		}

		public override void Update(GameTime gt)
		{
			if( !isAlive )
				return;
			base.Update(gt);
			lifeTime -= (float)gt.ElapsedGameTime.TotalSeconds;
			scale.X += scaleFactor * (float) gt.ElapsedGameTime.TotalSeconds;
			scale.Y += scaleFactor * (float)gt.ElapsedGameTime.TotalSeconds;
			if( lifeTime <= 0.0f )
				isAlive = false;
		}
	}
}
