using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DragSpider2 : MonoBehaviour
{
    public float force = 500f;
    Rigidbody2D selectedRigidBody;
    GameObject currentLeg;
    public LayerMask grabLayer;
    [SerializeField] float hTime = 3f;

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
        hTime -= 1 * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            hTime = 2;
            selectedRigidBody = GetRigidbodyFromMouseClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentLeg.GetComponent<LegController>().gTime = 3f;

            selectedRigidBody = null;
        }

        if (hTime < 0)
        {
            currentLeg = null;
            selectedRigidBody = null;
        }
    }

    Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, grabLayer);

        if(hit)
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null && hit.collider.gameObject.GetComponent<LegController>())
            {
                currentLeg = hit.collider.gameObject;
                currentLeg.GetComponent<LegController>().gTime = 0f;
                return hit .collider.gameObject.GetComponent<Rigidbody2D>();     
            }
        }

        return null;
    }
}
