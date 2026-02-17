using UnityEngine;

public class Tramp : MonoBehaviour
{
    [Header("Trampoline strength")]
    [SerializeField] private float strength = 15f;
    [SerializeField] private float step = 5f;
    [SerializeField] private float minStrength = 5f;
    [SerializeField] private float maxStrength = 50f;

    void Update()
    {
        // - = decrease strength
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            strength = Mathf.Max(minStrength, strength - step);
        // + = increase strength (numpad + or Shift+=)
        if (Input.GetKeyDown(KeyCode.KeypadPlus)
            || ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Equals))
            || (Input.inputString.Length > 0 && Input.inputString[0] == '+'))
            strength = Mathf.Min(maxStrength, strength + step);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        Rigidbody2D rb = col.collider.attachedRigidbody;
        if (rb == null) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, strength);
    }
}
