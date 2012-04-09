using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Moon.Helpers;
using Moon.Metro.Extensions;

namespace Moon.Metro.Helpers
{
    public static class NotificationHelper
    {
        static NotificationHelper()
        {
            BadgeUpdater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            TileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            DefaultExpirationtime = 60;
        }

        public static int DefaultExpirationtime { get; set; }
        public static BadgeUpdater BadgeUpdater { get; private set; }
        public static TileUpdater TileUpdater { get; private set; }

        public static void UpdateTile(TileTemplateType tileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            Guard.NotNull(text, images);

            var tileXml = TileUpdateManager.GetTemplateContent(tileTemplateType);

            Guard.NotNull(tileXml);

            UpdateTileText(text, tileXml);
            UpdateTileImages(images, tileXml);

            CreateAndUpdateNotification(tileXml);
        }

        public static void UpdateTiles(TileTemplateType tileTemplateType, TileTemplateType wideTileTemplateType, IEnumerable<string> text, IEnumerable<string> images)
        {
            Guard.NotNull(text, images);

            var tileXml = TileUpdateManager.GetTemplateContent(tileTemplateType);
            var wideTileXml = TileUpdateManager.GetTemplateContent(wideTileTemplateType);

            Guard.NotNull(tileXml, wideTileXml);

            UpdateTileText(text, tileXml, wideTileXml);
            UpdateTileImages(images, tileXml, wideTileXml);

            CreateAndUpdateNotification(tileXml, wideTileXml);
        }

        private static void CreateAndUpdateNotification(params XmlDocument[] tiles)
        {
            Guard.NotNull(tiles);
            var length = tiles.Length;
            Guard.True(length > 0);

            var tile = tiles[0];
            for (int i = 1; i < length; i++)
            {
                var tileBinding = tiles[i].GetElementsByTagName("binding").Item(0);
                IXmlNode node = tile.ImportNode(tileBinding, true);

                tile.GetElementsByTagName("visual").Item(0).AppendChild(node);
            }

            var tileNotification = CreateTileNotification(tile);
            tileNotification.Update();
        }

        private static TileNotification CreateTileNotification(XmlDocument tileXml)
        {
            return CreateTileNotification(tileXml, DefaultExpirationtime);
        }

        private static TileNotification CreateTileNotification(XmlDocument tileXml, int expirationTime)
        {
            var tileNotification = new TileNotification(tileXml);
            tileNotification.ExpireIn(expirationTime);

            return tileNotification;
        }

        private static void UpdateTileText(IEnumerable<string> text, params XmlDocument[] tiles)
        {
            var textCount = text.Count();

            if (textCount == 0) return;

            foreach (var tileXml in tiles)
            {
                var textAttributes = tileXml.GetElementsByTagName("text");

                for (int i = 0; i < textCount && i < textAttributes.Length; i++)
                {
                    textAttributes[i].InnerText = text.ElementAt(i);
                }
            }
        }

        private static void UpdateTileImages(IEnumerable<string> images, params XmlDocument[] tiles)
        {
            var imagesCount = images.Count();

            if (imagesCount == 0) return;
            
            foreach (var tileXml in tiles)
            {
                var imageAttributes = tileXml.GetElementsByTagName("image");

                for (int i = 0; i < imagesCount && i < imageAttributes.Length; i++)
                {
                    var element = (XmlElement)imageAttributes[i];
                    var source = "ms-resource:" + images.ElementAt(i);

                    element.SetAttribute("src", source);
                }
            }
        }
    }
}
