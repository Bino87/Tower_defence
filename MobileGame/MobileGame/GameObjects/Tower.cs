using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Description;
using MobileGame.Enums;
using MobileGame.GameObjects.Projectiles;
using MobileGame.GameObjects.Towers;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects
{

	public class Tower: Renderable, IGameObject, ITower
	{
		readonly float rangeSqrt;
		readonly int dmg;
		readonly float timeBetweenShoots;
		float timeSinceLastShot;
		public Tower(float range, int dmg, float timeBetweenShoots, RenderDesc renderDesc)
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
			var projectile = new Projectile(1f, 4f, dmg, enemy.Position, 350f, RenderDesc.CreateDrawDescriptin<TowerProjectile>(position));
			projectiles.Add(projectile);
			timeSinceLastShot = timeBetweenShoots;

			var pd = new ParticleDesc(ParticleEffectType.Shoot, enemy.Position - position, 17)
			{
				Radius = MapManager.TileSize/2,
				TextureIndex = TextureManager.GetTextureIndex(typeof(Particle))
			};
			ParticleEngine.AddEffect(this, pd);
			rotation = (float)Math.Atan2(enemy.Position.Y - position.Y, enemy.Position.X - position.X);
		}

		public bool IsInRange(Enemy enemy)
		{
			float rsqrt = Vector2.DistanceSquared(position, enemy.Position);
			return rsqrt<= rangeSqrt;
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
				return new TowerLight(200f, 10, 1f, RenderDesc.CreateDrawDescriptin<TowerLight>(pos));
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