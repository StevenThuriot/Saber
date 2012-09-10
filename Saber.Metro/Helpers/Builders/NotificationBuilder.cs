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

using Saber.Helpers;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Saber.Metro.Extensions;

namespace Saber.Metro.Helpers.Builders
{
    internal partial class NotificationBuilder
    {
        private XmlDocument _Tile;
        private uint _ExpirationTime;

        #region Flags

        private bool _SquareTileSet;
        private bool _WideTileSet;

        #endregion

        /// <summary>
        /// Default Ctor
        /// </summary>
        public NotificationBuilder()
        {
            _ExpirationTime = NotificationHelper.DefaultExpirationtime;
        }

        public NotificationBuilder SetExpirationTime(uint seconds)
        {
            _ExpirationTime = seconds;
            return this;
        }

        public  NotificationBuilder BuildSquareTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            BuildTile(false, tileTemplateType, text, images);            
            return this;
        }

        public NotificationBuilder BuildWideTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            BuildTile(true, tileTemplateType, text, images);
            return this;
        }

        private void BuildTile(bool isWide, TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            Guard.False(isWide ? _WideTileSet : _SquareTileSet);
            Guard.NotNull(text, images);

            var templateName = tileTemplateType.ToString().ToUpperInvariant();
            var containsString = isWide ? "WIDE" : "SQUARE";

            //Wide tiles have "Wide" in their name while normal tiles have "Square" in their name.
            //Making sure that the proper templates are used.
            Guard.True(templateName.Contains(containsString));


            var tileXml = TileUpdateManager.GetTemplateContent(tileTemplateType);

            Guard.NotNull(tileXml);

            UpdateTileText(tileXml, text);
            UpdateTileImages(tileXml, images);

            SetOrMergeTile(tileXml);

            if (isWide)
            {
                _WideTileSet = true;
            }
            else
            {
                _SquareTileSet = true;
            }
        }


        private void SetOrMergeTile(XmlDocument tileXml)
        {
            if (_Tile == null)
            {
                //Set
                _Tile = tileXml;
            }
            else
            {
                //Merge
                var tileBinding = tileXml.GetElementsByTagName("binding").Item(0);
                IXmlNode node = _Tile.ImportNode(tileBinding, true);

                _Tile.GetElementsByTagName("visual").Item(0).AppendChild(node);
            }
        }

        private static void UpdateTileText(XmlDocument tile, IEnumerable<string> text)
        {
            var textCount = text.Count();

            if (textCount == 0) return;

            var textAttributes = tile.GetElementsByTagName("text");

            for (int i = 0; i < textCount && i < textAttributes.Length; i++)
            {
                textAttributes[i].InnerText = text.ElementAt(i);
            }
        }

        private static void UpdateTileImages(XmlDocument tile, IEnumerable<string> images)
        {
            var imagesCount = images.Count();

            if (imagesCount == 0) return;

            var imageAttributes = tile.GetElementsByTagName("image");

            for (int i = 0; i < imagesCount && i < imageAttributes.Length; i++)
            {
                var element = (XmlElement)imageAttributes[i];
                var source = "ms-resource:" + images.ElementAt(i);

                element.SetAttribute("src", source);
            }
        }

        internal TileNotification Build()
        {
            var tileNotification = new TileNotification(_Tile);
            tileNotification.ExpireIn(_ExpirationTime);

            return tileNotification;
        }
    }
}
