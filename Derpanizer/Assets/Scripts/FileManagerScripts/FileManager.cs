using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Assets.Scripts.GameManagerScript;
using Assets.Scripts.SaveManager;
using FileManagerScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FileManagerScripts
{
	[Serializable]
	public class FileManager : MonoBehaviour
	{
		public static FileManager Manager;
		public List<StuffToSaveClass> FileList;
		public string RootPath;

		private const float InitMargin = 0.01f;

		private FileReader _reader;
		private Vector3 _defaultLocation;
		private List<DirectoryInfo> _directories;
		private bool _loaded = false;
		private List<GameObject> _objList;

		public void CommonInit()
		{
			_reader = new FileReader(RootPath);
			_defaultLocation = SetDefaultLocation();
			_directories = _reader.GetAllDirectories();
			_objList = new List<GameObject>();

			InitContainers();
			InitFiles();
		}

		public void InitFromLoadFile(SaveClass loadFile)
		{
			_loaded = true;
			RootPath = loadFile.Path;
			FileList = loadFile.FileList;
			CommonInit();
		}

		public void InitFromScratch(string path)
		{
			_loaded = false;
			RootPath = path;
			FileList = new List<StuffToSaveClass>();
			CommonInit();
		}

		private static Vector3 SetDefaultLocation()
		{
			var table = GameObject.Find(Const.Const.DEFAULT_TABLE);
			if (table != null)
			{
				var loc = table.transform.position;
				loc.y += table.GetComponent<Renderer>().bounds.size.y + InitMargin;
				return loc;
			}
			return new Vector3();
		}

		private void InitContainers()
		{
			var containers = GameObject.FindGameObjectsWithTag(Const.Const.CONTAINER);

			foreach (var container in containers)
			{
				var dir = DirectoryExists(container.name);
				if (dir != null)
				{
					container.GetComponent<MoveScript>().Init(dir);
				}
				else
				{
					//find right place in tree and create folder
					string parentFolder;

					if ((parentFolder = container.transform.parent.name).Equals(RootPath))
					{
						parentFolder = container.transform.name;
					}
					var parentDir = DirectoryExists(parentFolder);
					if (parentDir == null)
					{
						var rootdir = _reader.GetRootDirectory();
						rootdir.CreateSubdirectory(rootdir.FullName + Const.Const.BACKSLASH + parentFolder);
					}
					dir = parentDir.CreateSubdirectory(parentDir.FullName + Const.Const.BACKSLASH + container.name);
					container.GetComponent<MoveScript>().Init(dir);
				}

				if (!_loaded)
				{
					var filesinDirectory = dir.GetFiles();
					if (filesinDirectory.Length > 0)
					{
						AddFilesToList(filesinDirectory, container);
					}
				}
			}
		}

		private void InitFiles()
		{
			foreach (var file in FileList)
			{
				if (file.Info.Exists)
				{
					InstantiateFile(file);
				}
			}
		}

		private DirectoryInfo DirectoryExists(string containerName)
		{
			foreach (var directory in _directories)
			{
				if (directory.Name.Equals(containerName))
				{
					return directory;
				}
			}
			return null;
		}


		private void AddFilesToList(IEnumerable<FileInfo> infos, GameObject container)
		{
			foreach (var file in infos)
			{
				FileList.Add(ConvertToSerializable(file, container));
			}
		}

		private void InstantiateFile(StuffToSaveClass file)
		{
			GameObject obj;
			switch (file.Info.Extension)
			{
				case ".png":
				case ".jpg":
				case ".jpeg":
					obj = InstantiateImage(file);
					break;
				case ".txt":
				case ".doc":
				case "pdf":
				case "docx":
				case "rtf":
					obj = Instantiate(
							(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Textdocument.prefab", typeof(GameObject)), file.Location.ToVector3(), file.Rotation.ToQuaternion());
					break;
				default:
					obj = Instantiate(
							(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/File.prefab", typeof(GameObject)), file.Location.ToVector3(), file.Rotation.ToQuaternion());
					break;

			}

			obj.AddComponent<FileObject>();
			obj.GetComponent<FileObject>().Init(file.Info);

			//file.Obj = obj;
			_objList.Add(obj);

			// Set HoverText
			var hover = Instantiate(
					(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HoverT.prefab", typeof(GameObject)),
					obj.transform.position, file.Rotation.ToQuaternion());
			hover.GetComponent<HoverText>().SetTarget(obj.transform);
			hover.GetComponent<HoverText>().GetComponent<GUIText>().text = file.Info.Name.Split(Const.Const.BACKSLASH.ToCharArray()).Last();
		}

		private GameObject InstantiateImage(StuffToSaveClass file)
		{
			var obj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Image.prefab", typeof(GameObject));
			var loc = file.Location.ToVector3();
			loc.y += 1;
			obj = Instantiate(obj, loc, file.Rotation.ToQuaternion());
			obj.GetComponent<PictureResizer>().InitImage(file.Info.ToString());
			return obj;
		}

		private GameObject GetContainer(string dirName)
		{
			var containers = GameObject.FindGameObjectsWithTag(Const.Const.CONTAINER);
			foreach (var container in containers)
			{
				if (container.name.Equals(dirName))
				{
					return container;
				}
			}
			return null;
		}

		private static StuffToSaveClass ConvertToSerializable(FileInfo info, GameObject obj)
		{
			var pos = obj.transform.position;
			var position = new SerializableVector3(pos.x, pos.y, pos.z);
			var rot = obj.transform.rotation;
			var rotation = new SerializableQuaternion(rot.x, rot.y, rot.z, rot.w);
			return new StuffToSaveClass(position, rotation, info);
		}

		public void UpdateFileLocations()
		{
			var tempList = new List<StuffToSaveClass>();
			//foreach (var file in FileList)
			//{
			//	tempList.Add(ConvertToSerializable(file.Info, file.Obj));
			//}

			for (int i = 0; i < FileList.Count; i++)
			{
				tempList.Add(ConvertToSerializable(FileList.ElementAt(i).Info, _objList.ElementAt(i)));
			}
			FileList = tempList;
		}

		public void DeleteFile(FileInfo fileInfo)
		{
			for (int i = 0; i < FileList.Count; i++)
			{
				if (FileList.ElementAt(i).Info.Equals(fileInfo))
				{
					FileList.RemoveAt(i);
					_objList.RemoveAt(i);
				}
			}
		}
	}
}