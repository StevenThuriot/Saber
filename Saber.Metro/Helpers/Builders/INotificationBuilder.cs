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
using Windows.UI.Notifications;

namespace Saber.Metro.Helpers.Builders
{
    /// <summary>
    /// Interface for the notification builder class.
    /// </summary>
    public interface INotificationBuilder
    {
        /// <summary>
        /// Builds the square tile.
        /// </summary>
        /// <param name="tileTemplateType">The type of template to use.</param>
        /// <param name="text">The text to place on the tile.</param>
        /// <param name="images">The images to place on the tile.</param>
        INotificationBuilderSquareTileSet BuildSquareTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images);

        /// <summary>
        /// Builds the wide tile.
        /// </summary>
        /// <param name="tileTemplateType">The type of template to use.</param>
        /// <param name="text">The text to place on the tile.</param>
        /// <param name="images">The images to place on the tile.</param>
        INotificationBuilderWideTileSet BuildWideTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images);

        /// <summary>
        /// Sets the expiration time in seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds it will take to expire.</param>
        INotificationBuilder SetExpirationTime(uint seconds);
    }

    /// <summary>
    /// Interface for the notification builder class.
    /// </summary>
    public interface INotificationBuilderSquareTileSet
    {
        /// <summary>
        /// Builds the wide tile.
        /// </summary>
        /// <param name="tileTemplateType">The type of template to use.</param>
        /// <param name="text">The text to place on the tile.</param>
        /// <param name="images">The images to place on the tile.</param>
        INotificationBuilder BuildWideTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images);

        /// <summary>
        /// Sets the expiration time in seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds it will take to expire.</param>
        INotificationBuilderSquareTileSet SetExpirationTime(uint seconds);
    }

    /// <summary>
    /// Interface for the notification builder class.
    /// </summary>
    public interface INotificationBuilderWideTileSet
    {
        /// <summary>
        /// Builds the square tile.
        /// </summary>
        /// <param name="tileTemplateType">The type of template to use.</param>
        /// <param name="text">The text to place on the tile.</param>
        /// <param name="images">The images to place on the tile.</param>
        INotificationBuilderSquareTileSet BuildSquareTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images);

        /// <summary>
        /// Sets the expiration time in seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds it will take to expire.</param>
        INotificationBuilderWideTileSet SetExpirationTime(uint seconds);
    }


}
