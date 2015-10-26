namespace MobileGame.Interfaces
{
	interface IProjectile 
	{
		void DealDamage(IEnemy enemy);
		bool IsColiding(IEnemy enemy);
	}
}
