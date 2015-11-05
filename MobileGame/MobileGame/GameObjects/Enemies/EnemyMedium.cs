using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Description;

namespace MobileGame.GameObjects.Enemies
{

	class EnemyMedium : Enemy
	{
		public EnemyMedium(Queue <Vector2> path, int playerIndex, int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(path, playerIndex, healthPoints, destination, speed, renderDesc)
		{
		}
	}

}