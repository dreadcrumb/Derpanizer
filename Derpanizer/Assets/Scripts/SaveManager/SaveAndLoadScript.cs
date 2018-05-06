using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.FileManagerScripts;
using Assets.Scripts.SaveManager;
using Assets.Scripts.Util;
using FileManagerScripts;
using UnityEngine;

public class SaveAndLoadScript : MonoBehaviour
{
	#region Const

	private const string _savePath = "/fileInfo.dat";

	#endregion Const

	private static SaveAndLoadScript SAVE_SCRIPT;

	[SerializeField]
	private SaveClass _saveTo;

	// Use this for initialization
	void Start()
	{
		_saveTo = new SaveClass();
	}

	public void Save()
	{
		if (_saveTo != null)
		{
			var bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + _savePath, FileMode.OpenOrCreate);

			//_saveTo.fileList = list;
			var f = gameObject.GetComponentInParent<FileManager>();
			_saveTo.FileList = f.FileList;
			_saveTo.Path = f.RootPath;
			_saveTo.BoxList = GetAllBoxes();

			bf.Serialize(file, _saveTo);
			file.Close();
		}
	}

	private List<StuffToSaveClass> GetAllBoxes()
	{
		var returnList = new List<StuffToSaveClass>();
		var allBoxes = GameObject.FindGameObjectsWithTag("box");
		if (allBoxes != null)
		{
			returnList.AddRange(allBoxes.Select(box => Util.ConvertToSerializable(null, box)));
			return returnList;
		}
		else
		{
			return null;
		}
	}

	public SaveClass Load()
	{
		if (File.Exists(Application.persistentDataPath + _savePath))
		{
			var bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + _savePath, FileMode.Open);
			file.Position = 0;
			var loaded = (SaveClass)bf.Deserialize(file);
			file.Close();
			return loaded;
		}
		else
		{
			// Error Handling
			return null;
		}
	}


	private void OnDispose()
	{
		Save();
	}
}