using Microsoft.Xna.Framework;

namespace MobileGame.Managers
{
	public static class SpawnManager
	{
		static float spawnTime = 1f;
		static float timeSinceLastSpawn = 10f;

		public static bool SpawnEnemy(GameTime gt)
		{
			timeSinceLastSpawn -= (float)gt.ElapsedGameTime.TotalSeconds;

			if( timeSinceLastSpawn > 0 )
				return false;
			timeSinceLastSpawn +=spawnTime;
			return true;
		}

	}
}
