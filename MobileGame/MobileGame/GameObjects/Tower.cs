using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Tower: GameObject, ITower
	{
		readonly float rangeSqrt;
		float dmg;
		readonly float timeBetweenShoots;
		float timeSinceLastShot;
		public Tower(float range, float dmg,float timeBetweenShoots, RenderDesc renderDesc)
			: base(renderDesc)
		{
			rangeSqrt = range * range;
			this.dmg = dmg;
			this.timeBetweenShoots = timeBetweenShoots;
			timeSinceLastShot = 0.0f;
		}


		public override void Update(GameTime gt)
		{
			if(timeSinceLastShot > 0)
			{
				timeSinceLastShot -= (float)gt.ElapsedGameTime.TotalSeconds;
			}
		}

		public void Shoot(Enemy enemy, List <Projectile> projectiles )
		{
			//projectiles.Add(new Projectile(adad));
			timeSinceLastShot = timeBetweenShoots;
		}

		public bool IsInRange(Enemy enemy)
		{
			return (Vector2.DistanceSquared(position, enemy.Position) <= rangeSqrt);
		}

		public bool CanShoot()
		{
			return timeSinceLastShot <= 0.0f;
		}
	}
}