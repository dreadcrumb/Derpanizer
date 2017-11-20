using UnityEngine;

namespace Assets.Scripts
{
    public class CameraScript : MonoBehaviour
    {
        public float SpeedH = 2.5f;
        public float SpeedV = 2.5f;

        private float _yaw = 0.0f;
        private float _pitch = 0.0f;

        void Update()
        {
            _yaw += SpeedH * Input.GetAxis("Mouse X");
            _pitch -= SpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);

        }
    }
}