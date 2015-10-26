using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;
using MobileGame.Enums;
using MobileGame.EventArgs;
using MobileGame.Managers;
using ServiceStack.Text;

namespace MobileGame.GameObjects
{

	public class Player: GameObject, IPlayer
	{
		public event EventHandler <BuildTowerEventArgs> BuildTowerEvent;
		public event EventHandler <TakeDamageEventArgs> TakeDamageEvent;

		object key = new object();

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
		public Vector2 Position { get { return position; } set { position = value; } }
		public PlayerStatus Status
		{
			get
			{
				return playerStatus;
			}
			set
			{
				playerStatus = value;
				if( playerStatus == PlayerStatus.BuildTower )
				{
					// build tower
				}
			}
		}
		public int Gold { get { return gold; } set { gold = value; } }
		public int LivesLeft { get { return livesLeft; } set { livesLeft = value; } }
		public int Score { get { return score; } set { score = value; } }
		public int Kills { get { return kills; } set { kills = value; } }

		protected internal Player(int index, ClientManager cm, DrawDescription drawDescription)
			: base(drawDescription)
		{
			this.cm = cm;
			towers = new List<Tower>();
			enemies = new List<Enemy>();
			projectiles = new List<Projectile>();
			myPath = string.Format("{0}{1}", "Game/", index);
			livesLeft = int.MaxValue;
			gold = 300;
			playerStatus = PlayerStatus.Idle;
			CreateFirebaseSlot();
			TakeDamageEvent += OnTakeDamage;
			BuildTowerEvent += OnBuildTower;

		}


		void OnBuildTower(object sender, BuildTowerEventArgs e)
		{
			towers.Add(e.Tower);
			cm.Client.SetAsync(myPath, this);
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

		public void BuildTower(PlayerStatus pStatus, Tower tower)
		{

			if( playerStatus == pStatus )
				return;

			//Create logic allowing building towers



			if( BuildTowerEvent != null )
				BuildTowerEvent.Invoke(this, new BuildTowerEventArgs { PlayerStatus =  pStatus, Tower = tower });
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

		public override void Update(GameTime gt)
		{
			UpdateLists(gt);

			TowerShoot();

			ProjectileCollide();

			CleanUpLists();
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
				foreach( var enemy in enemies )
				{
					if( !enemy.IsAlive )
						continue;

					if( !tower.CanShoot(enemy) )
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

	}

}