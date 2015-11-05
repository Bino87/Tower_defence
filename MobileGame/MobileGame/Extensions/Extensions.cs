using System;
using Microsoft.Xna.Framework;

namespace MobileGame.Extensions
{
	public static class Extensions
	{
		public static Vector2 RotateVector(this Vector2 vec, float rotation)
		{
			vec.X = (float) (vec.X * Math.Cos(rotation) - vec.Y * Math.Sin(rotation));
			vec.Y = (float) (vec.X * Math.Sin(rotation) + vec.Y * Math.Cos(rotation));

			return vec;
		}


		public static float ToRadians(this float number)
		{
			return MathHelper.ToRadians(number);
		}


		public static float NextFloat(this Random rand, float min = 0, float max = float.MaxValue)
		{
			var precision = 1000000;
			var random = rand.Next((int)(min * precision),(int)( max * precision));
			return random / (float) precision;
		}
	}
}
