using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float upwardAngle = -45;
    public float downwardAngle = -135;
    public float verticalSpeed = 5f;
    private Vector2 direction;

    void Start()
    {
        direction = Vector2.down;
        transform.rotation = Quaternion.Euler(0, 0, downwardAngle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction.y = -direction.y;
            if (direction.y > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, upwardAngle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, downwardAngle);
            }
        }

        transform.Translate(Time.deltaTime * verticalSpeed * direction, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Wall"))
        {
            FindObjectOfType<GameController>().GameOver();
        }
    }
}
