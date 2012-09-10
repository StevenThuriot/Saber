#region License
// 
//  Copyright 2012 Steven Thuriot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Saber.Helpers;
using Saber.Metro.Extensions;
using Saber.Metro.Helpers.Builders;

namespace Saber.Metro.Helpers
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
