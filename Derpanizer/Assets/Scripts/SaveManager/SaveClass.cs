using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
	[Serializable]
	public class SaveClass
	{
		[SerializeField] public List<StuffToSaveClass> FileList;
		[SerializeField] public List<StuffToSaveClass> BoxList;
		[SerializeField] public string Path;
	}
}
