﻿using UnityEngine;

namespace Assets.Scripts
{
    public class MouseDrag : MonoBehaviour
    {
        private float distance = 15;

        void OnMouseDrag()
        {
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            var objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objectPosition;
        }
    }
}
