using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FileManagerScripts
{
	public class FileReader : MonoBehaviour
	{

		public string Path;
		public DirectoryInfo DirectoryInfo;
		public Transform File;


		public FileReader(string path)
		{
			Path = path;
			DirectoryInfo = new DirectoryInfo(path);
		}

		public FileInfo[] GetFileInfo(string path)
		{
			if (string.IsNullOrEmpty(Path))
			{
				// TODO : Error handling
				return new FileInfo[0];
			}


			//DirectoryInfo.GetDirectories();
			return DirectoryInfo.GetFiles();

		}

		public List<DirectoryInfo> GetAllDirectories()
		{
			var list = DirectoryInfo.GetDirectories().ToList();
			foreach (var dir in DirectoryInfo.GetDirectories())
			{
				list.AddRange(dir.GetDirectories().ToList());
			}
			return list;
		}

		public DirectoryInfo GetRootDirectory()
		{
			return DirectoryInfo;
		}
	}
}