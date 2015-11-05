using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.GameObjects;

namespace MobileGame.Managers
{
	public static class ParticleEngine
	{
		static List <ParticleEffect> particleEffects = new List <ParticleEffect>();


		public static void AddEffect<T>(ParticleEffectType pet, Vector2 position) where T : Renderable
		{
			var particleDesc = CreateParticleEffectDescription(pet);
		}


		static ParticleEffectDescription CreateParticleEffectDescription(ParticleEffectType pet)
		{
			switch(pet)
			{
			case ParticleEffectType.Shoot:
				break;
			case ParticleEffectType.Explode:
				break;
			case ParticleEffectType.Die:
				break;
			case ParticleEffectType.Escape:
				break;
			}

			return null;
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

	class ParticleEffectDescription
	{
		int amount;
		RenderDesc renderDesc;
	}

}
