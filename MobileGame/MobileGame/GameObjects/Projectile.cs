using Microsoft.Xna.Framework;
using MobileGame.Description;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Projectile : Moveable, IProjectile
	{
		float lifeTime;
		readonly float radiusSqrt;
		readonly int dmg;
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
			isAlive = false;
		}


		public bool IsColiding(Enemy enemy)
		{
			return Vector2.DistanceSquared(position, enemy.Position) < radiusSqrt + enemy.RadiusSqrt;
		}
	}

}