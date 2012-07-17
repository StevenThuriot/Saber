using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Saber.Helpers
{
	/// <summary>
	/// A directory helper class.
	/// </summary>
	public static class DirectoryManager
	{
		private static readonly StringComparer Comparer = StringComparer.Create(CultureInfo.CurrentCulture, true);

		private static bool AreEqual(string firstValue, string secondValue)
		{
			return Comparer.Compare(firstValue, secondValue) == 0;
		}

		/// <summary>
		/// Compare two equal directories and search for any deleted directories.
		/// </summary>
		/// <param name="originalDirectory">The original directory.</param>
		/// <param name="newDirectory">The new directory.</param>
		/// <returns>A list of deleted directories.</returns>
		public static IEnumerable<DirectoryInfo> FindDeletedDirectories(DirectoryInfo originalDirectory, DirectoryInfo newDirectory)
		{
			if (!Directory.Exists(newDirectory.FullName))
				return Enumerable.Empty<DirectoryInfo>();

			var originalDirectories = originalDirectory.GetDirectories();
			var newDirectories = newDirectory.GetDirectories();

			return originalDirectories.Where(x => !newDirectories.Any(y => AreEqual(y.Name, x.Name)));
		}

		/// <summary>
		/// Compare two equal directories and search for any deleted files.
		/// </summary>
		/// <param name="originalDirectory">The original directory.</param>
		/// <param name="newDirectory">The new directory.</param>
		/// <returns>A list of deleted directories.</returns>
		public static IEnumerable<FileInfo> FindDeletedFiles(DirectoryInfo originalDirectory, DirectoryInfo newDirectory)
		{
			var deletedFiles = new List<FileInfo>();

			if (!Directory.Exists(newDirectory.FullName))
				return Enumerable.Empty<FileInfo>();

			var droppedFiles = newDirectory.GetFiles();

			if (!originalDirectory.Exists)
			{
				deletedFiles.AddRange(droppedFiles);
				deletedFiles.AddRange(FindDeletedSubfiles(originalDirectory, newDirectory));
			}
			else
			{
				var originalFiles = originalDirectory.GetFiles();
				var deleted = droppedFiles.Where(x => !originalFiles.Any(y => AreEqual(y.Name, x.Name)));

				deletedFiles.AddRange(deleted);
				deletedFiles.AddRange(FindDeletedSubfiles(originalDirectory, newDirectory));
			}

			return deletedFiles;
		}

		private static IEnumerable<FileInfo> FindDeletedSubfiles(DirectoryInfo originalDirectory, DirectoryInfo droplocation)
		{
			var files = new List<FileInfo>();
			foreach (var directory in droplocation.GetDirectories())
			{
				var originalSubDirectoryString = Path.Combine(originalDirectory.FullName, directory.Name);
				var originalSubDirectory = new DirectoryInfo(originalSubDirectoryString);

				var subFiles = FindDeletedFiles(originalSubDirectory, directory);

				files.AddRange(subFiles);
			}
			return files;
		}

		/// <summary>
		/// Deletes a file.
		/// </summary>
		/// <param name="file">The file to delete.</param>
		public static void Delete(FileSystemInfo file)
		{
			if (File.Exists(file.FullName))
			{
				File.Delete(file.FullName);
			}
		}

		/// <summary>
		/// Cleans up a certain folder (deleting it and its content).
		/// </summary>
		/// <param name="folder">The folder to clean.</param>
		public static void Clean(DirectoryInfo folder)
		{
			if (!Directory.Exists(folder.FullName))
			{
				return;
			}

			foreach (var file in folder.GetFiles())
			{
				Delete(file);
			}

			foreach (var directory in folder.GetDirectories())
			{
				Directory.Delete(directory.FullName, true);
			}
		}

		/// <summary>
		/// Recursively copies the source folder to the target folder.
		/// </summary>
		/// <param name="source">The folder to copy.</param>
		/// <param name="target">The folder to copy to.</param>
		public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
		{
			if (!Directory.Exists(target.FullName))
			{
				Directory.CreateDirectory(target.FullName);
			}

			foreach (var file in source.GetFiles())
			{
				File.Copy(file.FullName, Path.Combine(target.ToString(), file.Name), true);
			}

			foreach (var directory in source.GetDirectories())
			{
				var nextTargetDir = target.CreateSubdirectory(directory.Name);
				CopyAll(directory, nextTargetDir);
			}
		}
	}

}
