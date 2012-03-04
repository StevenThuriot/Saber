using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Moon.Settings
{
    ///<summary>
    /// The Moon framework settings class.
    ///</summary>
    [DebuggerDisplay("Moon's Settings Class")]
    public static class Moon
    {
    	private static IMoonMultilanguage _Language = new MoonMultilanguage();

    	///<summary>
        /// In case the default resource implementation does not suffice (e.g. you desire a translation), it is possible to replace it by your own.
		/// You can reset it to the default setting by calling ResetLanguage().
        ///</summary>
        public static IMoonMultilanguage Language
    	{
    		get { return _Language; }
    		set { _Language = value; }
    	}

    	///<summary>
        /// In case you want to use Moon's implemented language file, but use ith with your own resource file, you can use this property to set it.
        /// In case you've set the Language property to a custom built class, setting this property will reset it to the default Moon class.
        ///</summary>
        public static void SetCustomResource(ResourceManager resourceManager)
        {
            if (resourceManager == null)
                return;

            var newKeys = new Collection<string>();

            var newResourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true).GetEnumerator();

            while (newResourceSet.MoveNext())
                newKeys.Add(newResourceSet.Key.ToString());

            var resourceType = typeof(MoonRes);
            var properties = resourceType.GetProperties(BindingFlags.Static | (resourceType.IsPublic
                                                                                       ? BindingFlags.Public
                                                                                       : BindingFlags.NonPublic));

            var foundAllKeys = properties.Select(x => x.Name)
                                       .Intersect(newKeys)
                                       .Count() == (properties.Length - 2);

            if (!foundAllKeys)
                return;

            var resourceManagerField = resourceType.GetField("resourceMan", BindingFlags.Static | BindingFlags.NonPublic);

            if (resourceManagerField == null)
                return;

            resourceManagerField.SetValue(null, resourceManager);

			ResetLanguageInstance();
        }

        /// <summary>
        /// Resets the Language property to Moon's default setting.
        /// </summary>
        public static void ResetLanguage()
        {
            var resourceManagerField = typeof (MoonRes).GetField("resourceMan", BindingFlags.Static | BindingFlags.NonPublic);

            if (resourceManagerField == null)
                return;

            resourceManagerField.SetValue(null, null);

			ResetLanguageInstance();
        }

		private static void ResetLanguageInstance()
		{
			if (Language == null || Language.GetType() != typeof(MoonMultilanguage))
			{
				Language = new MoonMultilanguage();
			}
		}
    }
}