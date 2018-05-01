using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	public float DoorOpenAngle = 90.0f;
	private float _doorClosedX;
	private float _doorClosedY;
	private float _doorClosedZ;
	public float DoorAnimSpeed = 2.0f;
	private Quaternion _doorOpen;
	private Quaternion _doorClose;
	public bool DoorStatus = false; //false is close, true is open
	private bool _doorGo = false; //for Coroutine, when start only one

	void Start()
	{
		_doorClosedX = gameObject.transform.rotation.x;
		_doorClosedY = gameObject.transform.rotation.y;
		_doorClosedZ = gameObject.transform.rotation.z;

		_doorOpen = Quaternion.Euler(_doorClosedX, _doorClosedY, DoorOpenAngle);
		_doorClose = Quaternion.Euler(_doorClosedX, _doorClosedY, _doorClosedZ);
		//Find only one time your player and get him reference
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