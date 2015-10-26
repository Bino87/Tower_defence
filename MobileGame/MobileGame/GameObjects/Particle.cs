using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{
	abstract class Particle: GameObject, IParticle
	{
		float lifeTime;
		readonly float speed;
		readonly Vector2 direction;


		protected Particle(float lifeTime, float speed, Vector2 direction, DrawDescription drawDescription)
			: base(drawDescription)
		{
			this.lifeTime = lifeTime;
			this.speed = speed;
			this.direction = direction;
		}

		public override void Update(GameTime gt)
		{
			if(!isAlive)
				return;
			lifeTime -= (float) gt.ElapsedGameTime.TotalSeconds;
			if(lifeTime <= 0.0f)
			{
				isAlive = false;
				return;
			}
			position += direction * speed * (float)gt.ElapsedGameTime.TotalSeconds;
		}
	}
}
