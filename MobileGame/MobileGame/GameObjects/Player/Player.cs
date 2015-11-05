using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Description;
using MobileGame.Enums;
using MobileGame.EventArgs;
using MobileGame.GameObjects.Enemies;
using MobileGame.GameObjects.Tiles;
using MobileGame.Interfaces;
using MobileGame.Managers;

namespace MobileGame.GameObjects.Player
{

	public class Player: Renderable, IGameObject, IPlayer
	{
		public event EventHandler <TakeDamageEventArgs> TakeDamageEvent;
		List <Enemy> enemies;
		List <Tower> towers;
		List <Projectile> projectiles;
		readonly ClientManager cm;
		readonly string myPath;
		PlayerStatus playerStatus;
		int kills;
		int gold;
		int livesLeft;
		int score;
		readonly int index;

		public PlayerStatus Status
		{
			get
			{
				return playerStatus;
			}
			set
			{
				playerStatus = value;
				if( playerStatus == PlayerStatus.BuildTowerLight )
				{
					// build tower
				}
			}
		}
		public int Gold { get { return gold; } set { gold = value; } }
		public int LivesLeft { get { return livesLeft; } set { livesLeft = value; } }
		public int Score { get { return score; } set { score = value; } }
		public int Kills { get { return kills; } set { kills = value; } }

		protected internal Player(int index, ClientManager cm, RenderDesc renderDesc)
			: base(renderDesc)
		{

			//RenderableDest.CreateRenderDesc<T>()
			//{
			//		int index = TextureManager.GetTextureIndex(typeof(T));
			//}
			this.cm = cm;
			towers = new List<Tower>();
			enemies = new List<Enemy>();
			projectiles = new List<Projectile>();
			this.index = index;
			myPath = string.Format("{0}{1}", "Game/", index);
			livesLeft = int.MaxValue;
			gold = 300;
			playerStatus = PlayerStatus.Idle;
			position = new Vector2(MapManager.GetXaxisOffser(index) + 50f, 200f);
			CreateFirebaseSlot();
			TakeDamageEvent += OnTakeDamage;

			/*for(int i = 0; i < 10; i++) //Debugger
			{
				for(int j = 0; j < 20; j++)
				{
					var tower = Tower.GetTowerInstance(new Vector2(i*MapManager.TileSize + MapManager.GetXaxisOffser(index), j*MapManager.TileSize), index, PlayerStatus.BuildTowerLight);
					towers.Add(tower);
				}
			}*/
		}


		public void SpawnEnemy(EnemyType et)
		{
			Enemy enemy = null;
			switch( et )
			{
			case EnemyType.Light:
				enemy = CreateEnemy<EnemyLight>();
				break;
			case EnemyType.Medium:
				enemy = CreateEnemy<EnemyMedium>();
				break;
			case EnemyType.Heavy:
				enemy = CreateEnemy<EnemyHeavy>();
				break;

			}
			if( enemy == null )
				return;

			enemies.Add(enemy);

		}

		Enemy CreateEnemy<TEnemyType>() where TEnemyType: Enemy
		{
			var type = typeof(TEnemyType);
			var pos = MapManager.Path.Peek();
			pos.X += MapManager.GetXaxisOffser(index);
			var dest = Vector2.Zero;

			if( type == typeof(EnemyLight) )
				return new EnemyLight(MapManager.Path, index, 100, dest, 100, RenderDesc.CreateDrawDescriptin<EnemyLight>(pos));
			if( type == typeof(EnemyMedium) )
				return new EnemyMedium(MapManager.Path, index, 200, dest, 75, RenderDesc.CreateDrawDescriptin<EnemyMedium>(pos));
			if( type == typeof(EnemyHeavy) )
				return new EnemyMedium(MapManager.Path, index, 400, dest, 50, RenderDesc.CreateDrawDescriptin<EnemyHeavy>(pos));

			return null;
		}

		void OnTakeDamage(object sender, TakeDamageEventArgs args)
		{
			livesLeft -= args.DamageTaken;
			Gold++;
			if( livesLeft <= 0 )
			{
				isAlive = false;
			}

			cm.Client.UpdateAsync(myPath, this);

		}

		public void BuildTower(PlayerStatus pStatus)
		{
			if( playerStatus == pStatus )
				return;
			if( CanBuildTower() )
			{
				playerStatus = PlayerStatus.OK;
				gold -= 50;
				var tower = Tower.GetTowerInstance(position, index, pStatus);
				if( tower != null )
				{
					if( towers.Any(t => t.Position == tower.Position) )
					{
						playerStatus = PlayerStatus.Failed;
						cm.Client.UpdateAsync(myPath, Manager.Players[index]);
						return;
					}
					towers.Add(tower);
				}
			}
			else
			{
				playerStatus = PlayerStatus.Failed;
			}

			cm.Client.UpdateAsync(myPath, Manager.Players[index]);
		}

		bool CanBuildTower()
		{
			var coord = MapManager.GetTileIndexFromPosition(position, index);

			return MapManager.IsPassable(coord);
		}

		public void TakeDamage(int dmgTaken)
		{
			if( TakeDamageEvent != null )
				TakeDamageEvent.Invoke(this, new TakeDamageEventArgs { DamageTaken =  dmgTaken });
		}

		public void CreateFirebaseSlot()
		{
			cm.Client.UpdateAsync(myPath, this);
		}

		public void SpawnEnemy(IEnemy enemy)
		{
			throw new NotImplementedException();
		}

		void ProjectileCollide()
		{
			foreach( var projectile in projectiles )
			{
				if( !projectile.IsAlive )
					continue;
				foreach( var enemy in enemies )
				{
					if( !enemy.IsAlive )
						continue;
					if( !projectile.IsColiding(enemy) )
						continue;
					projectile.DealDamage(enemy);
					break;
				}
			}
		}

		void TowerShoot()
		{
			foreach( var tower in towers )
			{
				if( !tower.IsAlive )
					continue;
				if( !tower.CanShoot() )
					continue;
				foreach( var enemy in enemies )
				{
					if( !enemy.IsAlive )
						continue;

					if( !tower.IsInRange(enemy) )
						continue;

					tower.Shoot(enemy, projectiles);
					break;
				}
			}
		}

		void CleanUpLists()
		{
			towers = towers.FindAll(t => t.IsAlive);
			enemies = enemies.FindAll(e => e.IsAlive);
			projectiles = projectiles.FindAll(p => p.IsAlive);
		}

		void UpdateLists(GameTime gt)
		{
			foreach( var enemy in enemies )
				enemy.Update(gt);
			foreach( var tower in towers )
				tower.Update(gt);
			foreach( var projectile in projectiles )
				projectile.Update(gt);
		}

		public void SpawnEnemy(Enemy enemy)
		{
			enemies.Add(enemy);
		}

		public void Update(GameTime gt)
		{
			try
			{

			UpdateLists(gt);

			TowerShoot();

			ProjectileCollide();

			CleanUpLists();
			}
			catch(Exception)
			{
				// ignored
			}
		}

		public override void Draw(SpriteBatch sb)
		{
			var coord = MapManager.GetTileIndexFromPosition(position, index);
			var pos = MapManager.Map[coord.X, coord.Y].Position;
			pos.X += MapManager.GetXaxisOffser(index);
			ITile temp = new Ground(RenderDesc.CreateDrawDescriptin<Ground>(pos, color : Color.Pink * 0.3f));
			try
			{

				temp.Draw(sb);
				foreach( var tower in towers )
					tower.Draw(sb);
				foreach( var enemy in enemies )
					enemy.Draw(sb);
				foreach( var projectile in projectiles )
					projectile.Draw(sb);
				base.Draw(sb);
			}
			catch(Exception)
			{
				// ignored
			}
		}

	}
}