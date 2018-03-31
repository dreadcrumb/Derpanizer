using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
	[Serializable]
	public class SaveClass
	{

		[SerializeField] public List<StuffToSaveClass> FileList;
		[SerializeField] public string Path;

		//[SerializeField] public FileManager _fileManager;
	}
}
