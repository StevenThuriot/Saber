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