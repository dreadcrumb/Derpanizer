using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class OpenDoor : MonoBehaviour
    {

        public float DoorOpenAngle = 90.0f;
        public float DoorCloseAngle = 0.0f;
        public float DoorClosedX = 0.0f;
        public float DoorAnimSpeed = 2.0f;
        private Quaternion _doorOpen = Quaternion.identity;
        private Quaternion _doorClose = Quaternion.identity;
        private Transform _playerTrans = null;
        public bool DoorStatus = false; //false is close, true is open
        private bool _doorGo = false; //for Coroutine, when start only one

        void Start()
        {
            DoorStatus = false; //door is open, maybe change
            //Initialization your quaternions
            _doorOpen = Quaternion.Euler(DoorClosedX, 0, DoorOpenAngle);
            _doorClose = Quaternion.Euler(DoorClosedX, 0, DoorCloseAngle);
            //Find only one time your player and get him reference
            _playerTrans = GameObject.Find("Player").transform;

            Collider c = GetComponent<Collider>();
            c.isTrigger = true;
        }

        void OnMouseDown()
        {
            
            if (DoorStatus)
            {
                //close door
                StartCoroutine(this.MoveDoor(_doorClose));
            }
            else
            {
                //open door
                StartCoroutine(this.MoveDoor(_doorOpen));
            }
        }


        void Update()
        {
            //If press F key on keyboard
            if (Input.GetKeyDown(KeyCode.F) && !_doorGo)
            {
                //Calculate distance between player and door
                //if (Vector3.Distance(_playerTrans.position, this.transform.position) < 500f)
                //{
                    if (DoorStatus)
                    {
                        //close door
                        StartCoroutine(this.MoveDoor(_doorClose));
                    }
                    else
                    {
                        //open door
                        StartCoroutine(this.MoveDoor(_doorOpen));
                    }
                //}
            }
        }

        public IEnumerator MoveDoor(Quaternion dest)
        {
            _doorGo = true;
            //Check if close/open, if angle less 4 degree, or use another value more 0
            while (Quaternion.Angle(transform.localRotation, dest) > 4.0f)
            {
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, dest, Time.deltaTime * DoorAnimSpeed);
                //UPDATE 1: add yield
                yield return null;
            }
            //Change door status
            DoorStatus = !DoorStatus;
            _doorGo = false;
            //UPDATE 1: add yield
            yield return null;
        }
    }
}