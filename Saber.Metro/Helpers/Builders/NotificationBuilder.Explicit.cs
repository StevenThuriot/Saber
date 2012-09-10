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

namespace Saber.Metro.Helpers.Builders
{
    internal partial class NotificationBuilder : INotificationBuilder, INotificationBuilderSquareTileSet, INotificationBuilderWideTileSet
    {
        //Explicit implementations so we can have a nice flow for the user!

        INotificationBuilderSquareTileSet INotificationBuilder.BuildSquareTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            return BuildSquareTile(tileTemplateType, text, images);
        }

        INotificationBuilderSquareTileSet INotificationBuilderWideTileSet.BuildSquareTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            return BuildSquareTile(tileTemplateType, text, images);
        }

        INotificationBuilderWideTileSet INotificationBuilder.BuildWideTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            return BuildWideTile(tileTemplateType, text, images);
        }

        INotificationBuilder INotificationBuilderSquareTileSet.BuildWideTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            return BuildWideTile(tileTemplateType, text, images);
        }
        
        INotificationBuilder INotificationBuilder.SetExpirationTime(uint seconds)
        {
            return SetExpirationTime(seconds);
        }

        INotificationBuilderSquareTileSet INotificationBuilderSquareTileSet.SetExpirationTime(uint seconds)
        {
            return SetExpirationTime(seconds);
        }

        INotificationBuilderWideTileSet INotificationBuilderWideTileSet.SetExpirationTime(uint seconds)
        {
            return SetExpirationTime(seconds);
        }
    }
}
