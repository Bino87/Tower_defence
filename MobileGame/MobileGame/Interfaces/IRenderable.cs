using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MobileGame.Interfaces
{

	public interface IRenderable
	{
		Vector2 Position { get; set; }
		void Draw(SpriteBatch sb);
	}

}