using MobileGame.Description;

namespace MobileGame.GameObjects.Towers
{
	public class TowerLight: Tower
	{
		public TowerLight(float range, int dmg, float timeBetweenShoots, RenderDesc renderDesc)
			: base(range, dmg, timeBetweenShoots, renderDesc)
		{
		}
	}
}
