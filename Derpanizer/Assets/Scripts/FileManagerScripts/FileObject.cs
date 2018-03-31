using System.IO;
using UnityEngine;

namespace Assets.Scripts.FileManagerScripts
{
	public class FileObject : MonoBehaviour
	{
		public FileInfo Info { get; set; }

		public GameObject ContainerInfo { get; set; }

		public FileObject(FileInfo info, GameObject containerInfo)
		{
			Info = info;
		}

		public void Init(FileInfo fileInfo)
		{
			Info = fileInfo;
			//gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
		}
	}
}