using Microsoft.Xna.Framework;
using MobileGame.Drawable;

namespace MobileGame.GameObjects.Enemies
{
	class EnemyHeavy : Enemy
	{
		public EnemyHeavy(int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(healthPoints, destination, speed, renderDesc)
		{
		}
	}

}
