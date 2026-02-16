using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float flapStrength;
    public float moveSpeed = 5f;
    private Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move.y += 1f;
        }
        if (move != Vector2.zero)
        {
            body.linearVelocity = move.normalized * moveSpeed;
        }
    }

}
