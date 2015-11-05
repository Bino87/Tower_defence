using MobileGame.Description;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects.Tiles
{
	class Ground : Renderable, ITile
	{
		public Ground(RenderDesc renderDesc)
			: base(renderDesc)
		{
		}


		public bool CanPlaceTower()
		{
			return true;
		}
	}
}
