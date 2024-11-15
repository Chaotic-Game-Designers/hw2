using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float tiltAngle = 45f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetMovement(Vector2.down);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            SetMovement(Vector2.up);
        else
            SetMovement(Vector2.down);
    }

    private void SetMovement(Vector2 direction)
    {
        float angle = tiltAngle * (direction == Vector2.up ? 1 : -1);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = transform.up * speed;
    }
}
