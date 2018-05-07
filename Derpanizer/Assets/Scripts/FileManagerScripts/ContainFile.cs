using UnityEngine;

namespace Assets.Scripts.FileManagerScripts
{
	public class ContainFile : MonoBehaviour {

		void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("file"))
			{
				other.transform.parent = transform;
			}
		}
	}
}
