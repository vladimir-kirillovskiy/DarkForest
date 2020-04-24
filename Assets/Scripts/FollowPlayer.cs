using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; 
    public float keepDistanse = 5.0f;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player) {

            GameObject p_go = player.gameObject;

            float diff = p_go.transform.position.x - transform.position.x;

            int p_fireLevel = player.GetComponent<PlayerController>().fireLevel;

            switch(p_fireLevel) {
                case 1:
                    keepDistanse = 2.0f;
                    break;
                case 2:
                    keepDistanse = 3.0f;
                    break;
                case 3:
                    keepDistanse = 4.0f;
                    break;
                case 4:
                    keepDistanse = 5.0f;
                    break;
                case 5:
                    keepDistanse = 5.5f;
                    break;
                default:
                    keepDistanse = 3.0f;
                    break;
            }
            
            Vector2 v = p_go.GetComponent<Rigidbody2D>().velocity;
            
            if (diff > keepDistanse) {
                rb.velocity = new Vector2(v.x * 1.2f, 0.0f);
            } else if (diff < keepDistanse) {
               rb.velocity = new Vector2(v.x * 0.6f, 0.0f);
            } else {
                rb.velocity = new Vector2(v.x, 0.0f);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
        }
    }
}
