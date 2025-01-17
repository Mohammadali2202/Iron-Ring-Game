using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Behaviour script for the HUD to be displayed when the player dies
// Author: Aiden

public class GameOverHUDBehaviour : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image loadImage;
    [SerializeField] Image quitImage;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject eventSysPrefab;

    private float t;
    private Color startColorBG;
    private Color startColorB;
    private float alpha = 0;

    private float fadeDuration = 2.5f;
    private float startTime;

    private void Start()
    {
        startColorBG = background.color;
        startColorB = loadImage.color;
        startTime = Time.time;
        GetEventSys();
    }

    private void Update()
    {
        // Gets the alpha
        t = (Time.time - startTime) / fadeDuration;
        alpha = Mathf.Lerp(0, 1, t);

        // Sets color of bg
        Color newColor = startColorBG;
        newColor.a = alpha;
        background.color = newColor;

        // Sets color of buttons
        Color newButtonColor = startColorB;
        newButtonColor.a = alpha;
        loadImage.color = newButtonColor;
        quitImage.color = newButtonColor;

        // Sets color of text
        Color newTextC = gameOverText.color; 
        newTextC.a = alpha;
        gameOverText.color = newTextC;
    }

    public void LoadLastSave()
    {
        InitializeGame.Load();
        //print(PlayerAttributes.CurrentScene);
        SceneManager.LoadScene(PlayerAttributes.CurrentScene);
    }

    public void QuitGame()
    {
        // Set scene to village so they're not stuck in a boss fight?
        //PlayerAttributes.CurrentScene = "Village";
        InitializeGame.Save();
        SceneManager.LoadScene("Main Menu");
    }

    private void GetEventSys()
    {
        if (GameObject.Find("EventSystem") == null || GameObject.FindGameObjectsWithTag("EventSystem").Length == 0)
        {
            Instantiate(eventSysPrefab);
        }
    }
}