﻿using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FileManagerScripts
{
	public class DeleteScript : MonoBehaviour
	{

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(Const.Const.FILE))
			{
				var fileInfo = other.GetComponent<FileObject>().Info;
				GameObject.Find("gameManager").GetComponent<FileManager>().DeleteFile(fileInfo);
				Destroy(other.gameObject);
				FileUtil.DeleteFileOrDirectory(fileInfo.ToString());
			}
		}
	}
}
