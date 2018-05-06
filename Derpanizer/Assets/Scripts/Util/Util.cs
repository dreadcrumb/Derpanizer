using System.IO;
using Assets.Scripts.SaveManager;
using UnityEngine;

namespace Assets.Scripts.Util
{
	public class Util
	{

		public static StuffToSaveClass ConvertToSerializable(FileInfo info, GameObject obj)
		{
			var pos = obj.transform.position;
			var position = new SerializableVector3(pos.x, pos.y, pos.z);
			var rot = obj.transform.rotation;
			var rotation = new SerializableQuaternion(rot.x, rot.y, rot.z, rot.w);
			return new StuffToSaveClass(position, rotation, info);
		}

	}
}