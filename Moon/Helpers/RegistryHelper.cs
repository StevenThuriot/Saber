using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Win32;
using System.Globalization;

namespace Moon.Helpers
{
    /// <summary>
    /// Defines how the registry helper searches for the passed value.
    /// </summary>
    public enum RegistryComparer
    {
        /// <summary>
        /// Match the passed value.
        /// </summary>
        Match,

		/// <summary>
		/// Contains the passed value.
        /// </summary>
        Contains
    }

    /// <summary>
    /// Helper class to make working with the Windows Registry easier.
    /// </summary>
    public static class RegistryHelper
    {
        /// <summary>
        /// Searches the registry for the passed type.
        /// </summary>
        /// <param name="type">The type to look for.</param>
        /// <returns>False if the default value of the found key is null or empty, else false.</returns>
        public static bool IsKnownType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return false;

            if (type[0] != '.')
                type = '.' + type;

            var openSubKey = Registry.ClassesRoot.OpenSubKey(type);
            
            if (openSubKey == null)
                return false;

            var value = Convert.ToString(openSubKey.GetValue(null), CultureInfo.CurrentCulture);

            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
		/// Looks through the uninstall keys in the registry to see if a certain piece of software is installed.
		/// Uses the "Contains" way of searching.
        /// </summary>
        /// <param name="name">The software to look for.</param>
        /// <returns>True if the software is found, else false.</returns>
		public static bool FindSoftware(string name)
        {
        	return FindSoftware(name, RegistryComparer.Contains);
        }

		/// <summary>
		/// Looks through the uninstall keys in the registry to see if a certain piece of software is installed.
		/// </summary>
		/// <param name="name">The software to look for.</param>
		/// <param name="comparer">How to search for the software.</param>
		/// <returns>True if the software is found, else false.</returns>
    	public static bool FindSoftware(string name, RegistryComparer comparer)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            //Current User
            var uninstallKeys = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (FindKey(uninstallKeys, name, comparer)) return true;

            //x86 Registry
            uninstallKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (FindKey(uninstallKeys, name, comparer)) return true;

            //x64 Registry
            uninstallKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
			if (FindKey(uninstallKeys, name, comparer)) return true;

            return false;
        }

        /// <summary>
        /// Checks if a passed path exists in the registry.
        /// </summary>
		/// <param name="root">The startpoint to start searching from. (e.g. Registry.LocalMachine)</param>
        /// <param name="path">The path to search for. (Relative)</param>
        /// <returns>True if the path exists.</returns>
        public static bool KeyExists(RegistryKey root, string path)
        {
            if (string.IsNullOrEmpty(path) || root == null)
                return false;

            var subKey = root.OpenSubKey(path);

            return subKey != null;
        }

    	/// <summary>
    	/// Searches for a certain key. Returns true if it exists.
    	/// Uses the "Contains" way of searching.
		/// </summary>
		/// <param name="root">The startpoint to start searching from. (e.g. Registry.LocalMachine)</param>
		/// <param name="key">The key to search.</param>
    	/// <returns>True if the key is found.</returns>
		public static bool FindKey(RegistryKey root, string key)
    	{
    		return FindKey(root, key, RegistryComparer.Contains);
    	}
		
		/// <summary>
		/// Searches for a certain key. Returns true if it exists.
		/// </summary>
		/// <param name="root">The startpoint to start searching from. (e.g. Registry.LocalMachine)</param>
		/// <param name="key">The key to search.</param>
		/// <param name="comparer">How to search for the software.</param>
		/// <returns>True if the key is found.</returns>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is desired behaviour.")]
		public static bool FindKey(RegistryKey root, string key, RegistryComparer comparer)
        {
            if (!string.IsNullOrEmpty(key) && root != null)
            {
                try
                {
                    Func<string, string, bool> compare;

                    if (comparer == RegistryComparer.Contains)
                    {
                        compare = (x, y) => x.Contains(y);
                    }
                    else
                    {
                        compare = (x, y) => x == y;
                    }
                    
                    using (root)
                    {
						var keyFound = root.GetSubKeyNames()
										   .Select(root.OpenSubKey)
										   .Where(x => x != null)
										   .Select(x => Convert.ToString(x.GetValue("DisplayName"), CultureInfo.CurrentCulture))
										   .Any(x => !string.IsNullOrEmpty(x) && compare(x, key));

						if (keyFound)
							return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
