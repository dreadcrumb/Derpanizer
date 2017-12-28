using UnityEngine;

namespace FileManagerScripts
{
    public class CopyScript : MonoBehaviour {

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("file"))
            {
                Instantiate(collision.gameObject);
            }
        }
    }
}
