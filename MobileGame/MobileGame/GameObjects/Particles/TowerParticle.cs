using Microsoft.Xna.Framework;
using MobileGame.Description;

namespace MobileGame.GameObjects.Particles
{
	class TowerParticle : Particle
	{
		public TowerParticle(float lifeTime, float speed, Vector2 direction, RenderDesc renderDesc)
			: base(lifeTime, speed, direction, renderDesc)
		{
		}
	}
}
