using System;
using Microsoft.Xna.Framework;
using MobileGame.Drawable;
using MobileGame.Interfaces;

namespace MobileGame.GameObjects
{

	public class Tower : GameObject, ITower
	{
		public Tower(DrawDescription drawDescription)
			: base(drawDescription)
		{
		}


		public override void Update(GameTime gt)
		{
			throw new NotImplementedException();
		}


		public void Shoot(IEnemy enemy)
		{
			throw new NotImplementedException();
		}


		public bool CanShoot(IEnemy enemy)
		{
			throw new NotImplementedException();
		}


		public int Range { get; set; }
		public int RangeSqrt { get; set; }
	}

}