using System;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Enemy: Moveable, IEnemy
	{
		int healthPoints;
		float radiusSqrt;
		public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
		public float RadiusSqrt { get { return radiusSqrt; } }


		public Enemy(Vector2 destination, float speed, RenderDesc renderDesc)
			: base(destination, speed ,renderDesc)
		{
		}


		public void Death()
		{
			throw new NotImplementedException();
		}
	}

}