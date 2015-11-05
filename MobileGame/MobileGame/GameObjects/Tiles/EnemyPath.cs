using MobileGame.Description;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects.Tiles
{
	class EnemyPath : Renderable, ITile
	{
		public EnemyPath(RenderDesc renderDesc)
			: base(renderDesc)
		{
		}


		public bool CanPlaceTower()
		{
			return false;
		}
	}
}
