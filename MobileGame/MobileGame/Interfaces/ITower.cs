using MobileGame.GameObjects;

namespace MobileGame.Interfaces
{

	public interface ITower 
	{
		void Shoot(Enemy enemy);
		bool CanShoot(Enemy enemy);

		int Range { get; set; }
		int RangeSqrt { get; set; }
	}

}