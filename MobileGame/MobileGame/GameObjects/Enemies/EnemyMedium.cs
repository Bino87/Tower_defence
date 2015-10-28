using Microsoft.Xna.Framework;
using MobileGame.Drawable;

namespace MobileGame.GameObjects.Enemies
{

	class EnemyMedium : Enemy
	{
		public EnemyMedium(int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(healthPoints, destination, speed, renderDesc)
		{
		}
	}

}