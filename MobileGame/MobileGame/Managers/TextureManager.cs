using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Managers
{
	static class TextureManager
	{
		static readonly Dictionary <Type, Texture2D> textures = new Dictionary <Type, Texture2D>();




		public static void Add(Type type, Texture2D texture)
		{
			textures.Add(type,texture);
		}


		public static Texture2D GetTexture(Type type)
		{
			return textures[type];
		}


		public static void Add(Type type)
		{
			throw new NotImplementedException();
		}
	}
}
