using System;
using Microsoft.Xna.Framework;
using MobileGame.Enums;
using MobileGame.GameObjects.Player;

namespace MobileGame.Extensions
{
	public static class Extensions
	{
		static readonly Random rnd = new Random();

		public static Vector2 RotateVector(this Vector2 vec, float rotation)
		{
			vec.X = (float) (vec.X * Math.Cos(rotation) - vec.Y * Math.Sin(rotation));
			vec.Y = (float) (vec.X * Math.Sin(rotation) + vec.Y * Math.Cos(rotation));

			return vec;
		}


		public static EnemyType GetRandomEnemyType(this Player player)
		{
			var random = rnd.Next(3);

			return (EnemyType) random;
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
