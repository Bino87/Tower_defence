using System.Collections.Generic;
using MobileGame.GameObjects;

namespace MobileGame.Interfaces
{

	public interface ITower
	{
		void Shoot(Enemy enemy, List <Projectile> projectiles);
		bool IsInRange(Enemy enemy);
		bool CanShoot();

	}

}