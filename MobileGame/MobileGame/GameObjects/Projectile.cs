using System;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Projectile : Moveable, IProjectile
	{
		float speed;
		float lifeTime;
		float radiusSqrt;
		readonly int dmg;
		Vector2 direction;
		public Projectile(float  lifeTime, float radius, int dmg, Vector2 destiantion,float speed,  RenderDesc renderDesc)
			: base(destiantion, speed, renderDesc)
		{
			this.dmg = dmg;
			radiusSqrt = radius * radius;
			this.lifeTime = lifeTime;
		}


		public override void Update(GameTime gt)
		{
			base.Update(gt);
			lifeTime -= (float) gt.ElapsedGameTime.TotalSeconds;
			if(lifeTime <= 0.0f)
				isAlive = false;
		}


		public void DealDamage(Enemy enemy)
		{
			enemy.HealthPoints -= dmg;
		}


		public bool IsColiding(Enemy enemy)
		{
			return Vector2.DistanceSquared(position, enemy.Position) < radiusSqrt + enemy.RadiusSqrt;
		}
	}

}