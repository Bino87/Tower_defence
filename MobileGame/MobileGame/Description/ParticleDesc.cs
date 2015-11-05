using MobileGame.Enums;
using Microsoft.Xna.Framework;

namespace MobileGame.Description
{

	public struct ParticleDesc
	{
		public ParticleEffectType ParticleEffectType { get; set; }
		public int TextureIndex { get; set; }
		public int Amount { get; set; }
		public Vector2 Direction { get; set; }
		public int Radius { get;  set; }


		public ParticleDesc(ParticleEffectType pet, Vector2 direction, int amount)
			: this()
		{
			Amount = amount;
			ParticleEffectType = pet;
			direction.Normalize();
			Direction = direction;
		}
	}

}