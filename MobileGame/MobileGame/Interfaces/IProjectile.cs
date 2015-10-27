using MobileGame.GameObjects;

namespace MobileGame.Interfaces
{
	interface IProjectile 
	{
		void DealDamage(Enemy enemy);
		bool IsColiding(Enemy enemy);
	}
}
