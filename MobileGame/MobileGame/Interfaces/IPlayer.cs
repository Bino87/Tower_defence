using Microsoft.Xna.Framework.Graphics;
using MobileGame.Enums;
using MobileGame.GameObjects;

namespace MobileGame.Interfaces
{

	interface IPlayer 
	{
		
		void TakeDamage(int dmgTaken);
		void BuildTower(PlayerStatus pStatus);
		void SpawnEnemy(Enemy enemy);
		void Draw(SpriteBatch sb);
		int Gold { get; set; }
		PlayerStatus Status { get; set; }
		int LivesLeft { get; set; }
		int Score { get; set; }
		int Kills { get; set; }
	}

}
