using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
	[Serializable]
	public class StuffToSaveClass
	{
		[SerializeField] public SerializableVector3 Location;
		[SerializeField] public SerializableQuaternion Rotation;
		[SerializeField] public FileInfo Info;

		public StuffToSaveClass(SerializableVector3 loc, SerializableQuaternion quat, FileInfo inf)
		{
			Location = loc;
			Rotation = quat;
			Info = inf;
		}
	}
}