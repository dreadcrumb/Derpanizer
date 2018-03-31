using System;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
	[Serializable]
	public class SerializableVector3
	{
		public float X;
		public float Y;
		public float Z;

		public SerializableVector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, Y, Z);
		}
	}

}