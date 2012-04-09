using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Moon.Helpers;
using Moon.Metro.Helpers;

namespace Moon.Metro.Extensions
{
    public static class NotificationExtensions
    {
		public static void Update(this TileNotification tileNotification)
		{
			NotificationHelper.TileUpdater.Update(tileNotification);
		}
		
		public static void ExpireIn(this TileNotification tileNotification, int seconds)
		{
            var expirationTime = DateTimeOffset.Now.AddSeconds(seconds);
            tileNotification.ExpirationTime = expirationTime;
		}
	}
}