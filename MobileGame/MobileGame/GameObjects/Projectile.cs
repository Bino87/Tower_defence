using System;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Projectile : GameObject, IProjectile
	{
		public Projectile(RenderDesc renderDesc)
			: base(renderDesc)
		{
		}


		public override void Update(GameTime gt)
		{
			throw new NotImplementedException();
		}


		public void DealDamage(IEnemy enemy)
		{
			throw new NotImplementedException();
		}


		public bool IsColiding(IEnemy enemy)
		{
			throw new NotImplementedException();
		}
	}

}