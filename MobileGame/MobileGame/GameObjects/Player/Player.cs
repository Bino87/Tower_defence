using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.EventArgs;
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
			this.cm = cm;
			towers = new List<Tower>();
			enemies = new List<Enemy>();
			projectiles = new List<Projectile>();
			this.index = index;
			myPath = string.Format("{0}{1}", "Game/", index);
			livesLeft = int.MaxValue;
			gold = 300;
			playerStatus = PlayerStatus.Idle;
			position = new Vector2(index * MapManager.TileSize * 10 + index * MapManager.Spread + 100, 300f);
			CreateFirebaseSlot();
			TakeDamageEvent += OnTakeDamage;
			projectiles.Add(new Projectile(10, 10, 10, new Vector2(1000), 10, RenderDesc.CreateDrawDescriptin(TextureManager.GetTextureIndex(typeof(Projectile)), Vector2.One)));

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
					towers.Add(tower);
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
			cm.Client.SetAsync(myPath, this);
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
			UpdateLists(gt);

			TowerShoot();

			ProjectileCollide();

			CleanUpLists();

			if(Status == PlayerStatus.OK)
			{
				Status = PlayerStatus.BuildTowerLight + 1000;
				cm.Client.UpdateAsync(myPath, Manager.Players[index]);
			}
		}

		public override void Draw(SpriteBatch sb)
		{
			var coord = MapManager.GetTileIndexFromPosition(position, index);
			var t = MapManager.Map[coord.X, coord.Y].Position;
			t.X += MapManager.GetXaxisOffser(index);
			ITile temp = new Ground(RenderDesc.CreateDrawDescriptin(TextureManager.GetTextureIndex(typeof(Ground)), t, color : Color.Pink, depth : 0.0f));

			temp.Draw(sb);
			foreach( var tower in towers )
				tower.Draw(sb);
			foreach( var enemy in enemies )
				enemy.Draw(sb);
			foreach( var projectile in projectiles )
				projectile.Draw(sb);
			base.Draw(sb);
		}

	}
}