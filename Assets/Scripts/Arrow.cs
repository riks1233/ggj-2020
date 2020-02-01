using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    float targetX, targetY;
    private Rigidbody2D rb;
    private float moveSpeed = 0.5f;
    private Vector2 directionVector;
    private void OnCollisionEnter(Collision collision)
    {
        collision.GetType();
        Destroy(this);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //targetX = targetY = 10;
        //directionVector = new Vector2(targetX - transform.position.x, targetY - transform.position.y).normalized;

        //float rotationAngle = Vector2.SignedAngle(Vector2.right, directionVector);
        //print(rotationAngle);
        //transform.Rotate(new Vector3(0, 0, rotationAngle));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + directionVector * moveSpeed * Time.fixedDeltaTime);
    }

    public void Setup(Vector2 directionVector)
    {
        this.directionVector = directionVector;
        float rotationAngle = Vector2.SignedAngle(Vector2.right, directionVector);
        transform.Rotate(new Vector3(0, 0, rotationAngle));
    }

    
}
