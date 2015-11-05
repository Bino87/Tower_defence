using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Enemy: Moveable, IEnemy
	{
		int healthPoints;
		readonly Queue <Vector2> path;
		readonly float radiusSqrt;
		readonly float offset;
		readonly int ownerIndex;
		Vector2 destination;
		public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
		public float RadiusSqrt { get { return radiusSqrt; } }


		public Enemy(Queue<Vector2> path, int playerIndex, int healthPoints, Vector2 destination, float speed, RenderDesc renderDesc)
			: base(destination, speed, renderDesc)
		{
			ownerIndex = playerIndex;
			offset = MapManager.GetXaxisOffser(playerIndex);
			position.Y -= MapManager.TileSize;
			this.path = new Queue<Vector2>(path);
			this.healthPoints = healthPoints;
			var radius = renderDesc.BoundingBox.Width / 2;
			radiusSqrt = radius * radius;
			this.destination = this.path.Dequeue();
			this.destination.X += offset;
			direction = this.destination - position;
			direction.Normalize();
		}


		public override void Update(GameTime gt)
		{
			if( (destination - position).LengthSquared() < Vector2.One.LengthSquared() )
			{
				if(MoveToNextNode())
					return;
			}
			else
				base.Update(gt);
			if( healthPoints <= 0 )
				Death();
		}

		bool MoveToNextNode()
		{
			position = destination;
			if(path.Count == 0)
			{
				DealDamage();
				return true;
			}
			destination = path.Dequeue();
			destination.X += offset;
			direction = destination - position;
			direction.Normalize();
			rotation = Rotation();
			return false;
		}

		float Rotation()
		{
			return (float) Math.Atan2(direction.Y, direction.X) - MathHelper.PiOver2; // Pi/2 needed cos of texture rotation could be fixed by rotating texture 90^ clockwise.
		}

		void DealDamage()
		{
			Manager.Players[ownerIndex].LivesLeft--;
			Death();
		}

		public virtual void Death()
		{
			isAlive = false;
		}
	}
}