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
