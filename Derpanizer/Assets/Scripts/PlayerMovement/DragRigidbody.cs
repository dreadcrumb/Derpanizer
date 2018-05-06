
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragRigidbody : MonoBehaviour
{
	const float k_Spring = 1000.0f;
	const float k_Damper = 5.0f;
	const float k_Drag = 1000.0f;
	const float k_AngularDrag = 5.0f;
	const float k_Distance = 0.3f;
	const bool k_AttachToCenterOfMass = false;

	private SpringJoint m_SpringJoint;
	private Rigidbody hitRigidbody;
	private float _distance;

	void OnMouseOver()
	{
		Debug.Log(transform.parent);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (hitRigidbody != null)
			{
				FreezeRotation();
				if (Input.GetKeyDown(KeyCode.Q))
				{
					_distance++;
				}
				else if (Input.GetKeyDown(KeyCode.E))
				{
					_distance--;
				}
				if (Input.GetMouseButton(1))
				{
					Camera.main.GetComponentInParent<FirstPersonController>().SetMouseRotation(false);
					hitRigidbody.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 10);
				}
				else
				{
					Camera.main.GetComponentInParent<FirstPersonController>().SetMouseRotation(true);
				}
				return;
			}

			var mainCamera = Camera.main;

			// We need to actually hit an object
			var hit = new RaycastHit();
			if (
				!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
					mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
					Physics.DefaultRaycastLayers))
			{
				return;
			}

			// We need to hit a rigidbody that is not kinematic
			if (!hit.rigidbody || hit.rigidbody.isKinematic)
			{
				return;
			}

			hitRigidbody = hit.rigidbody;

			//hitRigidbody.transform.parent = mainCamera.transform;

			if (!m_SpringJoint)
			{
				var go = new GameObject("Rigidbody dragger");
				var body = go.AddComponent<Rigidbody>();
				m_SpringJoint = go.AddComponent<SpringJoint>();
				body.isKinematic = true;
			}

			m_SpringJoint.transform.position = hit.point;
			m_SpringJoint.anchor = Vector3.zero;

			m_SpringJoint.spring = k_Spring;
			m_SpringJoint.damper = k_Damper;
			m_SpringJoint.maxDistance = k_Distance;
			m_SpringJoint.connectedBody = hit.rigidbody;

			_distance = hit.distance;
			StartCoroutine("DragObject");
		}
		else
		{
			if (hitRigidbody != null)
			{
				hitRigidbody.transform.parent = null;
				hitRigidbody.useGravity = true;
				hitRigidbody = null;
				UnFreezeRotation();
			}
			Camera.main.GetComponentInParent<FirstPersonController>().SetMouseRotation(true);
			return;
		}
	}

	private void FreezeRotation()
	{
		GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	private void UnFreezeRotation()
	{
		GetComponent<Rigidbody>().freezeRotation = false;
	}

	private IEnumerator DragObject()
	{
		// set outline while dragged
		// m_SpringJoint.connectedBody.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");

		var oldDrag = m_SpringJoint.connectedBody.drag;
		var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
		m_SpringJoint.connectedBody.drag = k_Drag;
		m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
		var mainCamera = Camera.main;
		while (Input.GetMouseButton(0))
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_SpringJoint.transform.position = ray.GetPoint(_distance);
			yield return null;
		}
		if (m_SpringJoint.connectedBody)
		{
			m_SpringJoint.connectedBody.drag = oldDrag;
			m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
			//m_SpringJoint.connectedBody.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			m_SpringJoint.connectedBody = null;
		}
	}
}