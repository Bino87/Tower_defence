using MobileGame.Drawable;

namespace MobileGame.GameObjects.Towers
{
	public class TowerLight: Tower
	{
		public TowerLight(float range, float dmg, float timeBetweenShoots, RenderDesc renderDesc)
			: base(range, dmg, timeBetweenShoots, renderDesc)
		{
		}
	}
}
