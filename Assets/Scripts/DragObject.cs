using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;  

            switch (touch.phase)
            {
                // inicio
                case TouchPhase.Began:
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        offset = transform.position - touchPosition;
                    }
                    break;

                // durante
                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = touchPosition + offset;
                    }
                    break;

                // final 
                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}
