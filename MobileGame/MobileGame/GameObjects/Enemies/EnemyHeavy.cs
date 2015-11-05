using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;

namespace MobileGame.GameObjects.Enemies
{
	class EnemyHeavy : Enemy
	{
		public EnemyHeavy(Queue <Vector2> path, int playerIndex, int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(path, playerIndex, healthPoints, destination, speed, renderDesc)
		{
		}
	}

}
