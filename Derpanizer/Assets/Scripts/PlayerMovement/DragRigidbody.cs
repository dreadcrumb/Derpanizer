using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragRigidbody : MonoBehaviour
{
	const float k_Spring = 100.0f;
	const float k_Damper = 5.0f;
	const float k_Drag = 10.0f;
	const float k_AngularDrag = 5.0f;
	const float k_Distance = 0.2f;
	const bool k_AttachToCenterOfMass = false;

	private Transform rotatedPosition;

	private SpringJoint m_SpringJoint;
	private bool isDragging;

	public GameObject textField;
	public GameObject textField2;

	void Start()
	{
	}

	void OnMouseOver()
	{
		if (!isDragging && gameObject.CompareTag(Const.Const.FILE))
		{
			gameObject.GetComponent<Renderer>().material.shader = Shader.Find(Const.Const.SHILOUETTE_SHADER);
			if (textField != null)
			{
				textField.transform.position = gameObject.transform.position;
				textField.GetComponent<Text>().text = "game";
			}

			if (textField2 != null)
			{
				textField2.GetComponent<Text>().text = "holo";
			}
		}
	}

	void OnMouseExit()
	{
		if (!isDragging && gameObject.CompareTag(Const.Const.FILE))
		{
			gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			if (textField != null)
			{
				textField.transform.position = gameObject.transform.position;
				textField.GetComponent<Text>().text = "";
			}

			if (textField2 != null)
			{
				textField2.GetComponent<Text>().text = "";
			}

		}
	}

	private void Update()
	{
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		var mainCamera = FindCamera();

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

		rotatedPosition = mainCamera.transform;

		StartCoroutine("DragObject", hit.distance);
	}


	private IEnumerator DragObject(float distance)
	{
		isDragging = true;

		// set outline while dragged
		m_SpringJoint.connectedBody.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");

		var oldDrag = m_SpringJoint.connectedBody.drag;
		var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
		m_SpringJoint.connectedBody.drag = k_Drag;
		m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
		var mainCamera = FindCamera();
		while (Input.GetMouseButton(0))
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_SpringJoint.transform.position = ray.GetPoint(distance);
			yield return null;
		}
		if (m_SpringJoint.connectedBody)
		{
			m_SpringJoint.connectedBody.drag = oldDrag;
			m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
			m_SpringJoint.connectedBody.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			m_SpringJoint.connectedBody = null;
		}

		gameObject.transform.LookAt(rotatedPosition);

		if (rotatedPosition != null)
		{
			if (Input.GetKeyDown("q"))
			{
				//gameObject.transform.Rotate(Vector3.up, 10);
				rotatedPosition.Rotate(Vector3.up, 15);
			}
			else if (Input.GetKeyDown("e"))
			{
				//gameObject.transform.Rotate(Vector3.up, -10);
				rotatedPosition.Rotate(Vector3.up, -15);
			}

			gameObject.transform.LookAt(rotatedPosition);
		}

		isDragging = false;
	}


	private Camera FindCamera()
	{
		if (GetComponent<Camera>())
		{
			return GetComponent<Camera>();
		}

		return Camera.main;
	}
}