using Microsoft.Xna.Framework;
using MobileGame.Description;

namespace MobileGame.GameObjects.Projectiles
{
	class TowerProjectile : Projectile
	{
		public TowerProjectile(float lifeTime, float radius, int dmg, Vector2 destiantion, float speed, RenderDesc renderDesc)
			: base(lifeTime, radius, dmg, destiantion, speed, renderDesc)
		{
		}
	}
}
