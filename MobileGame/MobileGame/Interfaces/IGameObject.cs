using Microsoft.Xna.Framework;

namespace MobileGame.Interfaces
{

	public interface IGameObject 
	{
		void Update(GameTime gt);
		bool IsAlive { get; }

	}

}
