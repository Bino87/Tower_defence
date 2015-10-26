


using MobileGame.Enums;
using MobileGame.GameObjects;

namespace MobileGame.EventArgs
{

	public class BuildTowerEventArgs: System.EventArgs
	{
		public PlayerStatus PlayerStatus { get; set; }
		public Tower Tower { get; set; }
	}
}
