using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{
	public class ParticleEffect
	{
		List <Particle> partices;

		public ParticleEffect(int amount)
		{
			partices = new List<Particle>();

			// ReSharper disable once EmptyForStatement
			for( int i = 0; i < amount; i++ )
			{
				// var particle = new Particle...
				//particles.Add(particle);
			}
		}



		public void Update(GameTime gt)
		{
			foreach( var particle in partices )
			{
				particle.Update(gt);
			}
			CleanUpList();
		}

		void CleanUpList()
		{
			partices = partices.FindAll(p => p.IsAlive);
		}

		public void Draw(SpriteBatch sb)
		{
			foreach( var particle in partices )
			{
				particle.Draw(sb);
			}
		}

		public bool IsEmpty()
		{
			return partices.Count == 0;
		}
	}
}
