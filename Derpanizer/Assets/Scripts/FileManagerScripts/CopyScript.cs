using UnityEngine;

namespace FileManagerScripts
{
    public class CopyScript : MonoBehaviour
    {

        private bool _hasLeft = true;
        private bool _isInside = true;

        public Collider boxCollider;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            if (_hasLeft && _isInside/* && !collision.gameObject.tag.Contains("file")*/)
            {
                Instantiate(collision.gameObject, gameObject.transform);
                _hasLeft = false;

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _isInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _hasLeft = true;
            _isInside = false;
        }
    }
}
