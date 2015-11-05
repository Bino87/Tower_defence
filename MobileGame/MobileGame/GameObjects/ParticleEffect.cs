using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.GameObjects
{
	public class ParticleEffect
	{
		List <Particle> partices;


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


		public void Add(Particle particle)
		{
			if(partices == null)
				partices = new List <Particle>();
			partices.Add(particle);
		}

		public bool IsEmpty()
		{
			return partices.Count == 0;
		}
	}
}
