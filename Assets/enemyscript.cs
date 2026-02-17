using UnityEngine;

public class enemyscript : MonoBehaviour
{
    private Rigidbody2D body;
    public float moveSpeed = 3f;
    private Collider2D collison;
    int ctr = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collison.gameObject.tag="Death";
    }

    // Update is called once per frame
    void Update()
    {   
        if (ctr > 30) {
                ctr = 0;
                int num = Random.Range(0, 2);
                if (num == 0) {
                    body.linearVelocity = new Vector2(-moveSpeed, body.linearVelocity.y);
                } else {
                    body.linearVelocity = new Vector2(moveSpeed, body.linearVelocity.y);
                }
        }
        ctr++;
    }
}
