using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Tower: GameObject, ITower
	{
		float range;
		readonly float rangeSqrt;
		float dmg;
		float fireRate;
		float timeSinceLastShot;
		public Tower(float range, float dmg,float fireRate, DrawDescription drawDescription)
			: base(drawDescription)
		{
			this.range = range;
			this.rangeSqrt = range * range;
			this.dmg = dmg;
			this.fireRate = fireRate;
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
			timeSinceLastShot = fireRate;
		}


		public bool CanShoot(Enemy enemy)
		{
			if(timeSinceLastShot <= 0.0f)
				return false;
			return !(Vector2.DistanceSquared(position, enemy.Position) <= rangeSqrt) && false;
		}
	}

}