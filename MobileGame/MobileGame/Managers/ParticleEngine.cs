using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Description;
using MobileGame.Drawable;
using MobileGame.Enums;
using MobileGame.GameObjects;
using MobileGame.Extensions;
using MobileGame.GameObjects.Tiles;

namespace MobileGame.Managers
{
	public static class ParticleEngine
	{
		static List <ParticleEffect> particleEffects = new List<ParticleEffect>();
		static readonly Random rand = new Random();

		public static void AddEffect<T>(T sender, ParticleDesc partDesc) where T: Renderable
		{
			switch( partDesc.ParticleEffectType )
			{
			case ParticleEffectType.Shoot:
				CreateShootParticleEffect(sender as Tower, partDesc);
				break;
			case ParticleEffectType.Explode:
				CreateExplodeParticleEffect(sender as Projectile, partDesc);
				break;
			case ParticleEffectType.Die:
				CreateDieParticleEffect(sender as Enemy, partDesc);
				break;
			case ParticleEffectType.Escape:
				CreateEscapeParticleEffect(sender as Enemy, partDesc);
				break;
			}
		}


		static void CreateEscapeParticleEffect<T>(T enemy, ParticleDesc pd) where T: Enemy
		{

		}


		static void CreateDieParticleEffect<T>(T enemy, ParticleDesc pd) where T: Enemy
		{

		}


		static void CreateExplodeParticleEffect<T>(T projectile, ParticleDesc pd) where T: Projectile
		{

		}


		static void CreateShootParticleEffect<T>(T tower, ParticleDesc pd) where T: Tower
		{
			ParticleEffect particleEffect = new ParticleEffect();
			for(int i = 0; i < pd.Amount; i++)
			{
				var position = pd.Direction * pd.Radius + tower.Position;
				var rendDesc = RenderDesc.CreateDrawDescriptin <Ground>(position,new Rectangle(0,0,2,2), color: Color.Pink);
				var lifetime = rand.NextFloat(.1f, 1);
				var speed = rand.NextFloat(5, 50);
				var angle = rand.NextFloat(-5, 6).ToRadians();
				var direction = pd.Direction.RotateVector(angle);

				var particle = new Particle(lifetime, speed, direction, rendDesc);

				particleEffect.Add(particle);
			}
			particleEffects.Add(particleEffect);
		}


		public static void Update(GameTime gt)
		{
			foreach( var particleEffect in particleEffects )
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
