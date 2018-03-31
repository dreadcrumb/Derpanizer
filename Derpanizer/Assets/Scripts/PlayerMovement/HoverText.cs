using UnityEngine;

public class HoverText : MonoBehaviour
{

    public Transform target;     // Object that this label should follow
    public Vector3 offset;    // Units in world space to offset; 1 unit above object by default
    public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
    public float clampBorderSize = .05f;  // How much viewport space to leave at the borders when a label is being clamped
    public bool useMainCamera = true;   // Use the camera tagged MainCamera
    public Camera cameraToUse;   // Only use this if useMainCamera is false

    private Camera cam;
    private Transform thisTransform;
    private Transform camTransform;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Use this for initialization
    void Start()
    {
        thisTransform = transform;
        if (useMainCamera)
            cam = Camera.main;
        else
            cam = cameraToUse;
        camTransform = cam.transform;
            
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (clampToScreen)
            {
                var relativePosition = camTransform.InverseTransformPoint(target.position);
                relativePosition.z = Mathf.Max(relativePosition.z, 1.0f);
                thisTransform.position =
                    cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
                thisTransform.position = new Vector3(
                    Mathf.Clamp(thisTransform.position.x, clampBorderSize, 1.0f - clampBorderSize),
                    Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1.0f - clampBorderSize),
                    thisTransform.position.z);
            }
            else
            {
                thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
            }
        }
    }
}
