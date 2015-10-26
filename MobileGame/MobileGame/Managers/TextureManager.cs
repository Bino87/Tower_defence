using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Managers
{
	static class TextureManager
	{
		static Texture2D[] textures = new Texture2D[0];
		static Dictionary <Type,int> textureIndexes = new Dictionary<Type, int>();


		public static void LoadTextures(ContentManager content)
		{
			var files = Directory.GetFiles(content.RootDirectory);

			for( var index = 0; index < files.Length; index++ )
			{
				var extension = Path.GetExtension(files[index]);
				if( extension != null && !extension.Equals(".png") )
					continue;
				var fileName = Path.GetFileNameWithoutExtension(files[index]);
				var texture = content.Load<Texture2D>(fileName);
				Add(index, texture);
				var type = Type.GetType(string.Format("MobileGame.GameObjects.{0}", fileName));
				textureIndexes.Add(type, index);
			}
		}


		public static int GetTextureIndex(Type type)
		{
			return textureIndexes.ContainsKey(type) ? textureIndexes[type] : 0;
		}


		static void Add(int index, Texture2D texture)
		{
			if( index == textures.Length )
				Array.Resize(ref textures, index + 1);
			textures[index] = texture;
		}


		public static Texture2D GetTexture(int index)
		{
			return textures[index];
		}
	}
}
