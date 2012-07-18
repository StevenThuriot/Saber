using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Saber.Helpers;
using Saber.Metro.Helpers;

namespace Saber.Metro.Extensions
{
    public static class NotificationExtensions
    {
        /// <summary>
        /// Sends the tile notification.
        /// </summary>
        /// <param name="tileNotification">The tile notification to send.</param>
		public static void Send(this TileNotification tileNotification)
		{
			NotificationHelper.TileUpdater.Update(tileNotification);
		}
		
        /// <summary>
        /// Sets the expiration time for a tilenotification in seconds.
        /// </summary>
        /// <param name="tileNotification">The tile notification.</param>
        /// <param name="seconds">The expiration time in seconds.</param>
		public static void ExpireIn(this TileNotification tileNotification, uint seconds)
		{
            var expirationTime = DateTimeOffset.Now.AddSeconds(seconds);
            tileNotification.ExpirationTime = expirationTime;
		}
	}
}