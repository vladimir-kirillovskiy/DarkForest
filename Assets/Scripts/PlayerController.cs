using UnityEngine;
using  UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public LayerMask groundMask;
    public Transform groundCheck;
    public Transform tourch;
    public bool isGrounded = true;
    public float groundDistance = 0.4f;
    public float speed = 50.0f;
    public float jumpHeight = 5.0f;
    public int extraJumps = 1;
	public float fallMultiplier = 1.5f;
	public float lowJumpMultiplier = 1.0f;
    public int fireLevel = 2;

    private Rigidbody2D rb;
    private bool isDead = false;
    private GameController gameController;
    private float timer;
    private int score;
    private bool facingRight = false;
    private float outerRadius = 2.0f;
    private int orbCount = 2;
    private int playerTimer = 1;

    private AudioSource[] audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponents<AudioSource>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if(gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("Cannot find GameController's script");
		}

    }

    void Update () {

        // fire level limited to 5
        fireLevel = orbCount <= 5 ? orbCount:5;
        
        if (orbCount == 0) {
            Destroy(gameObject);
            isDead = true;
            gameController.GameOver();
        }

        timer += Time.deltaTime;

        if (timer > 1.0f) {
            score = 1;
            gameController.AddScore(score);
            timer = 0.0f;
            speed++;
            if (playerTimer % 20 == 0) {
                orbCount--;
                gameController.AddOrbs(-1);
            }
            playerTimer++;
        }
    
        if (isDead) {
            return;
        }

        float currentRad = tourch.Find("Point Light 2D").GetComponent<Light2D>().pointLightOuterRadius;
        if (currentRad < outerRadius) {
            ++tourch.Find("Point Light 2D").GetComponent<Light2D>().pointLightOuterRadius;
        } else if (currentRad > outerRadius) { 
            --tourch.Find("Point Light 2D").GetComponent<Light2D>().pointLightOuterRadius;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
        if (isGrounded) {
            extraJumps = 2;
            animator.SetBool("isJumping", false);
        }

        float moveHorizontal = 1; // Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.velocity = new Vector2(moveHorizontal * speed * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && (isGrounded || extraJumps > 0)) {
            animator.SetBool("isJumping", true);
            rb.velocity = Vector2.up * jumpHeight;
            extraJumps--;
        }

        if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !(Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
        
        if (facingRight && moveHorizontal >= 0.5f) {
			Flip();
		} else if (!facingRight && moveHorizontal <= -0.5f) {
			Flip();
		}

        UpdateTorch();
    }

    private void Flip() {
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Orb")) {
            audioSource[0].Play();
            Destroy(other.gameObject);
            if (orbCount < 5) {
                orbCount++;
                gameController.AddOrbs(+1);
            }
        }
         if (other.gameObject.CompareTag("Enemy")) {
            audioSource[1].Play();
            Destroy(other.gameObject);
            gameController.AddScore(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            isDead = true;
            gameController.GameOver();
        }
    }

    private void UpdateTorch() {
        switch(fireLevel) {
            case 1:
                tourch.localScale = new Vector3(0.2f, 0.1f, 1.0f);
                tourch.localPosition = new Vector3(tourch.localPosition.x, 0.12f, tourch.localPosition.z);
                outerRadius = 2;
               break;
            case 2:
                tourch.localScale = new Vector3(0.3f, 0.2f, 1.0f);
                tourch.localPosition = new Vector3(tourch.localPosition.x, 0.223f, tourch.localPosition.z);
                outerRadius = 3;
                break;
            case 3:
                tourch.localScale = new Vector3(0.4f, 0.3f, 1.0f);
                tourch.localPosition = new Vector3(tourch.localPosition.x, 0.3f, tourch.localPosition.z);
                outerRadius = 4;
                break;
            case 4:
                tourch.localScale = new Vector3(0.5f, 0.4f, 1.0f);
                tourch.localPosition = new Vector3(tourch.localPosition.x, 0.33f, tourch.localPosition.z);
                outerRadius = 6;
                break;
            case 5:
                tourch.localScale = new Vector3(0.6f, 0.5f, 1.0f);
                tourch.localPosition = new Vector3(tourch.localPosition.x, 0.433f, tourch.localPosition.z);
                outerRadius = 8;
                break;
            default:
                tourch.localScale = new Vector3(0.3f, 0.2f, 1.0f);
                tourch.localPosition = new Vector3(tourch.position.x, 0.223f, tourch.position.z);
                outerRadius = 2;
                break;
        }
    }
}
