using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.GameObjects.Towers;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Tower: Renderable, IGameObject, ITower
	{
		readonly float rangeSqrt;
		float dmg;
		readonly float timeBetweenShoots;
		float timeSinceLastShot;
		public Tower(float range, float dmg, float timeBetweenShoots, RenderDesc renderDesc)
			: base(renderDesc)
		{
			rangeSqrt = range * range;
			this.dmg = dmg;
			this.timeBetweenShoots = timeBetweenShoots;
			timeSinceLastShot = 0.0f;
		}


		public virtual void Update(GameTime gt)
		{
			if( timeSinceLastShot > 0 )
			{
				timeSinceLastShot -= (float)gt.ElapsedGameTime.TotalSeconds;
			}
		}

		public void Shoot(Enemy enemy, List<Projectile> projectiles)
		{
			//projectiles.Add(new Projectile(adad));
			timeSinceLastShot = timeBetweenShoots;
		}

		public bool IsInRange(Enemy enemy)
		{
			return (Vector2.DistanceSquared(position, enemy.Position) <= rangeSqrt);
		}

		public bool CanShoot()
		{
			return timeSinceLastShot <= 0.0f;
		}


		public static Tower GetTowerInstance(Vector2 position, int index, PlayerStatus pStatus)
		{
			var coord = MapManager.GetTileIndexFromPosition(position, index);
			switch( pStatus )
			{
			case PlayerStatus.BuildTowerLight:
				var pos = MapManager.Map[coord.X, coord.Y].Position;
				pos.X += MapManager.GetXaxisOffser(index);
				return  new TowerLight(90f,10f,1f,RenderDesc.CreateDrawDescriptin(TextureManager.GetTextureIndex(typeof(TowerLight)),pos));
				break;
			case PlayerStatus.BuildTower2:
				break;
			case PlayerStatus.BuildTower3:
				break;
			case PlayerStatus.BuildTower4:
				break;
			}
			return null;
		}
	}
}