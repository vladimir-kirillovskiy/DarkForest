using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
	public TextMeshProUGUI restartText;
	public TextMeshProUGUI gameOverText;
	public TextMeshProUGUI OrbText;
    private bool gameOver, restart;
	private int score;
	private int orbs;
	private AudioSource[] audioSource;



    void Start()
    {
		audioSource = GetComponents<AudioSource>();
		
        gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		orbs = 2;
		UpdateScore();
		UpdateOrbs();
		
    }

    void Update() {
		// if (restart) {
		// 	if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {
		// 		Scene scene = SceneManager.GetActiveScene(); 
        //         SceneManager.LoadScene(scene.name);
		// 	}
		// }

        if(gameOver) {
			audioSource[0].Stop();
            
			Invoke( "ChangeScene", 1.0f );
			// restartText.text = "Tap to Try Again or Esc to Exit";
			restartText.text = "";
            restart = true;

			// if (Input.GetKeyDown(KeyCode.Escape)) {
			// 	// open menu
			// 	SceneManager.LoadScene(0); 
			// }
        }
	}

    public void AddScore(int NewScoreValue) {
		score += NewScoreValue;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score; 
	}

	public void AddOrbs(int NewOrbValue) {
		orbs += NewOrbValue;
		UpdateOrbs();
	}

	void UpdateOrbs() {
		OrbText.text = "X " + orbs; 
	}

    public void GameOver() {
		audioSource[1].Play();
		gameOverText.text = "You Can't Escape!";
		gameOver = true;
	}

	void ChangeScene() {
		SceneManager.LoadScene(0); 
	}
}
