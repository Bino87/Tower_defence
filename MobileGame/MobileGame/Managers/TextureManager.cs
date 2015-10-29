using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Managers
{
	static class TextureManager
	{
		static Texture2D[] textures = new Texture2D[0];
		static readonly Dictionary <Type,int> textureIndexes = new Dictionary<Type, int>();


		public static void LoadTextures(ContentManager content)
		{
			var directories = Directory.GetDirectories(content.RootDirectory);

			var files = new List <string>();
			foreach(var directory in directories)
			{
				files.AddRange(Directory.GetFiles(directory).Where(file => Path.GetExtension(file).Equals(".png")));
			}

			for( var index = 0; index < files.Count; index++ )
			{
				var fileName = files[index].Substring(8);
				fileName = fileName.Substring(0, fileName.Length - 4);
				var texture = content.Load<Texture2D>(fileName);
				Add(index, texture);
				var stringType = string.Format("MobileGame.GameObjects.{0}", fileName).Replace("\\", ".");
				var type = Type.GetType(stringType);
				if( type != null && !textureIndexes.ContainsKey(type) )
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
