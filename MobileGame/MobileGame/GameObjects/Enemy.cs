using System;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Enemy : GameObject, IEnemy
	{
		public Enemy(DrawDescription drawDescription)
			: base(drawDescription)
		{
		}


		public override void Update(GameTime gt)
		{
			throw new NotImplementedException();
		}


		public void Death()
		{
			throw new NotImplementedException();
		}
	}

}