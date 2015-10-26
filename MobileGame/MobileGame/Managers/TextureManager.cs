using System;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Managers
{
	static class TextureManager
	{
		static Texture2D[] textures = new Texture2D[0];


		public static void Add(int index, Texture2D texture)
		{
			if(index == textures.Length)
				Array.Resize(ref textures,index + 1 );
			textures[index] = texture;
		}


		public static Texture2D GetTexture(int index)
		{
			return textures[index];
		}
	}
}
