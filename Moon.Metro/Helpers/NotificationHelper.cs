using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Moon.Helpers;
using Moon.Metro.Extensions;
using Moon.Metro.Helpers.Builders;

namespace Moon.Metro.Helpers
{
    /// <summary>
    /// A Helper class to make updating your tiles in Metro a lot easier.
    /// </summary>
    public static class NotificationHelper
    {
        /// <summary>
        /// Static Ctor
        /// </summary>
        static NotificationHelper()
        {
            TileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            DefaultExpirationtime = 60;
        }

        /// <summary>
        /// The default expiration time of your notification expressed in seconds.
        /// </summary>
        public static uint DefaultExpirationtime { get; set; }

        /// <summary>
        /// A Tile Updater instance. (TileUpdateManager.CreateTileUpdaterForApplication())
        /// </summary>
        public static TileUpdater TileUpdater { get; private set; }

        /// <summary>
        /// Creates a tile notification using the Notification Builder.
        /// </summary>
        /// <param name="build">How to build the notification</param>
        /// <returns></returns>
        public static TileNotification Create(Action<INotificationBuilder> build)
        {
            var builder = new NotificationBuilder();
            build(builder);

            return builder.Build();
        }        
    }
}
