using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // This ensures Text refers to UnityEngine.UI.Text

// Removed the problematic using directive
// using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText; // Now clearly UnityEngine.UI.Text
    [SerializeField] private Text scoreText;    // Now clearly UnityEngine.UI.Text
    [SerializeField] private Text livesText;    // Now clearly UnityEngine.UI.Text

    [SerializeField] GameObject gameOverHUD;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    public static bool Eaten = false;

    private void Awake()
    {
        Instance = this;
        Eaten = false;
        /*
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        */
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        /*
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        */
    }

    private void NewGame()
    {
        //SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        //gameOverText.enabled = false;

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        //this.lives = lives;
        //livesText.text = "Lives: " + lives.ToString();
    }

    private void SetScore(int score)
    {
        //this.score = score;
        //scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        //pacman.DeathSequence();
        if (!Eaten)
        {
            Eaten = true;
            Instantiate(gameOverHUD);
        }

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            //pacman.gameObject.SetActive(false);

            // Win condition
            PlayerAttributes.GlobalGameState = 4;
            PlayerAttributes.PacComplete = true;
            SceneManager.LoadScene("DarkForestGraveyard");
            


            //Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
