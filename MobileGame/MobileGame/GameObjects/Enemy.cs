using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Enemy: Moveable, IEnemy
	{
		int healthPoints;
		readonly float radiusSqrt;
		public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
		public float RadiusSqrt { get { return radiusSqrt; } }


		public Enemy(int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(destination, speed ,renderDesc)
		{
			this.healthPoints = healthPoints;
			var radius = renderDesc.BoundingBox.Width / 2;
			radiusSqrt = radius * radius;
		}


		public void Death()
		{
		}
	}

}