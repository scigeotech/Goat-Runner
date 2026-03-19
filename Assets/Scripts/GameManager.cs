using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip scoreSound; //sound effect for scoring
    [SerializeField] private AudioClip gameOverSound; //sound effect for game over
    private AudioSource audioSource; // Audio source to play sound effects  
    public static GameManager Instance { get; private set; }
    public float initialSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float gameSpeed { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public TextMeshProUGUI scoreText;
    private int score;

    private Player player;
    private Spawner spawner;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        spawner = FindFirstObjectByType<Spawner>();
        audioSource = GetComponent<AudioSource>();

        StartGame();
    }

    public void StartGame()
    {
        Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
        foreach(var obstacle in obstacles) {
            Destroy (obstacle.gameObject); //destroy any existing obstacles in the scene
        }

        score = 0;
        scoreText.text = "Score: " + score + " spirit";

        gameSpeed = initialSpeed;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        audioSource.Stop(); //stop any existing sounds when starting a new game
    }

    public void Gameover()
    {
        gameSpeed = 0f; //stop the game by setting speed to 0
        enabled = false; //disable the GameManager to stop all updates
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        audioSource.clip = gameOverSound;
        audioSource.Play();

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        score += (int)gameSpeed;
        scoreText.text = "Score: " + score + " spirit"; //only update once increased, not necessary per frame
        audioSource.clip = scoreSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        gameSpeed += speedIncreaseRate * Time.deltaTime; //gradually increase game speed over time
    }
}
