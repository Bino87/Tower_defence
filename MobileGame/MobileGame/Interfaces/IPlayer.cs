using Microsoft.Xna.Framework;
using MobileGame.Enums;
using MobileGame.GameObjects;


namespace MobileGame.Interfaces
{

	interface IPlayer 
	{
		
		void TakeDamage(int dmgTaken);
		void BuildTower(PlayerStatus pStatus, Tower tower);
		void SpawnEnemy(Enemy enemy);
		int Gold { get; set; }
		Vector2 Position { get; set; }
		PlayerStatus Status { get; set; }
		int LivesLeft { get; set; }
		int Score { get; set; }
		int Kills { get; set; }
	}

}
