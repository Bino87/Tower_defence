using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace MobileGame.Managers
{
	class ClientManager
	{
		readonly IFirebaseConfig config;
		IFirebaseClient client;
		public ClientManager(string basepath, string authSecret = null)
		{
			config = new FirebaseConfig
			{
				BasePath = basepath
			};
			if( authSecret != null )
				config.AuthSecret = authSecret;
			client = new FirebaseClient(config);
		}


		public IFirebaseClient Client { get { return client; } set { client = value; } }
	}
}
