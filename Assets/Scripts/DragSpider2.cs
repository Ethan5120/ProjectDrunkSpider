using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSpider2 : MonoBehaviour
{
    public float force = 500f;
    Rigidbody2D selectedRigidBody;

    private void FixedUpdate()
    {
        if (selectedRigidBody)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) -selectedRigidBody.transform.position;
            selectedRigidBody.velocity = dir * force * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidBody = GetRigidbodyFromMouseClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedRigidBody = null;
        }
    }

    Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero);

        if(hit)
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                return hit .collider.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        return null;
    }
}
