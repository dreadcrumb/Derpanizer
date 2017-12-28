using UnityEngine;

namespace Assets.Scripts
{
    public class MouseDrag : MonoBehaviour
    {
        private float distance = 10;

        void OnMouseDrag()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objectPosition;
        }
    }
}
