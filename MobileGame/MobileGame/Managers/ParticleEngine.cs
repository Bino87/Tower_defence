using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.GameObjects;

namespace MobileGame.Managers
{
	public static class ParticleEngine
	{
		static List <ParticleEffect> particleEffects = new List <ParticleEffect>();


		public static void AddEffect()
		{
			
		}


		public static void Update(GameTime gt)
		{
			foreach(var particleEffect in particleEffects)
			{
				particleEffect.Update(gt);
			}
		}


		public static void CleanUpList()
		{
			particleEffects = particleEffects.FindAll(pe => !pe.IsEmpty());
		}


		public static void Draw(SpriteBatch sb)
		{
			foreach( var particleEffect in particleEffects )
			{
				particleEffect.Draw(sb);
			}
		}
	}
}
