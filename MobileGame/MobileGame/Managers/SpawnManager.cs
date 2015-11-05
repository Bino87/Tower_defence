using Microsoft.Xna.Framework;

namespace MobileGame.Managers
{
	public static class SpawnManager
	{
		static float spawnTime = 2f;
		static float timeSinceLastSpawn = 10f;

		public static bool SpawnEnemy(GameTime gt)
		{
			timeSinceLastSpawn -= (float)gt.ElapsedGameTime.TotalSeconds;

			if( timeSinceLastSpawn > 0 )
				return false;
			timeSinceLastSpawn +=spawnTime;
			if( spawnTime > 0.5f )
				spawnTime -= 0.01f;
			return true;
		}

	}
}
