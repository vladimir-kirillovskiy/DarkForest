using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(1 * speed * Time.deltaTime, rb.velocity.y);
    }

    // void OnBecameVisible()
    // {
    //     Debug.Log("Visible");
    //     enabled = true;
    //     gameObject.SetActive(true);
    // }
    // void OnBecameInvisible()
    // {
    //     Debug.Log("invisible");
    //     enabled = false;
    //     gameObject.SetActive(false);
    // }
}
