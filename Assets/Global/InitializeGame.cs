using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

// Initializes the game - sets up the save file, and sets the initial player attributes
//      - will be called when the game is loaded for the first time
// Author: Aiden

public class InitializeGame : MonoBehaviour
{
    // The data to be saved

    // The current state of the game - essentially the # of bosses defeated
    private static int gameState;

    // The current scene the player is in, at their save point
    private static string scene;

    // The players level and xp
    private static int level;
    private static int xp;

    // Other attributes of the player
    private static float playerHealth;
    private static float playerSpeed;
    private static float playerDefence;
    private static float playerDamage;

    private static bool playerAlive;

    // Player color
    private static Color playerColor;

    // dialog triggers
    private static bool startScene;
    private static bool golemComplete;
    private static bool kongComplete;
    private static bool pacComplete;
    private static bool magicianDisappeared;
    private static bool metAduoForp;
    private static bool pongComplete;

    private static int numOfFood;

    private void Awake()
    {
        Load();
    }

    private class SaveObject
    {
        public int gameState;
        public string scene;
        public int level;
        public int xp;
        public float playerHealth;
        public float playerSpeed;
        public float playerDefence;
        public float playerDamage;
        public Color playerColor;
        public bool playerAlive;
        
        public bool startScene;
        public bool golemComplete;
        public bool kongComplete;
        public bool pacComplete;
        public bool magicianDisappeared;
        public bool metAduoForp;
        public bool pongComplete;

        public int numOfFood;

    }

    public static void Save()
    {
        gameState = PlayerAttributes.GlobalGameState;
        scene = PlayerAttributes.CurrentScene;
        level = PlayerAttributes.PlayerLevel;
        xp = PlayerAttributes.PlayerXP;
        playerHealth = PlayerAttributes.PlayerHealth;
        playerSpeed = PlayerAttributes.PlayerSpeed;
        playerDefence = PlayerAttributes.PlayerDefence;
        playerDamage = PlayerAttributes.PlayerDamage;
        playerColor = PlayerAttributes.PlayerColor;
        playerAlive = PlayerAttributes.Alive;
        
        // flags for dialog triggers
        startScene = PlayerAttributes.StartScene;
        golemComplete = PlayerAttributes.GolemComplete;
        kongComplete = PlayerAttributes.KongComplete;
        pacComplete = PlayerAttributes.PacComplete;
        magicianDisappeared = PlayerAttributes.MagicianDisappeared;
        metAduoForp = PlayerAttributes.MetAduoForp;
        pongComplete = PlayerAttributes.PongComplete;

        numOfFood = PlayerAttributes.NumOfFood;

        SaveObject saveObject = new SaveObject
        {
            gameState = gameState,
            scene = scene,
            level = level,
            xp = xp,
            playerHealth = playerHealth,
            playerSpeed = playerSpeed,
            playerDefence = playerDefence,
            playerDamage = playerDamage,
            playerColor = playerColor,
            playerAlive = playerAlive,

            startScene = startScene,
            golemComplete = golemComplete,
            kongComplete = kongComplete,
            pacComplete = pacComplete,
            magicianDisappeared = magicianDisappeared,
            metAduoForp = metAduoForp,
            pongComplete = pongComplete,
            numOfFood = numOfFood
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }
    public static void Save(string nextScene)
    {
        gameState = PlayerAttributes.GlobalGameState;
        scene = nextScene;
        level = PlayerAttributes.PlayerLevel;
        xp = PlayerAttributes.PlayerXP;
        playerHealth = PlayerAttributes.PlayerHealth;
        playerSpeed = PlayerAttributes.PlayerSpeed;
        playerDefence = PlayerAttributes.PlayerDefence;
        playerDamage = PlayerAttributes.PlayerDamage;
        playerColor = PlayerAttributes.PlayerColor;
        playerAlive = PlayerAttributes.Alive;

        // flags for dialog triggers
        startScene = PlayerAttributes.StartScene;
        golemComplete = PlayerAttributes.GolemComplete;
        kongComplete = PlayerAttributes.KongComplete;
        pacComplete = PlayerAttributes.PacComplete;
        magicianDisappeared = PlayerAttributes.MagicianDisappeared;
        metAduoForp = PlayerAttributes.MetAduoForp;
        pongComplete = PlayerAttributes.PongComplete;

        numOfFood = PlayerAttributes.NumOfFood;

        SaveObject saveObject = new SaveObject
        {
            gameState = gameState,
            scene = scene,
            level = level,
            xp = xp,
            playerHealth = playerHealth,
            playerSpeed = playerSpeed,
            playerDefence = playerDefence,
            playerDamage = playerDamage,
            playerColor = playerColor,
            playerAlive = playerAlive,

            startScene = startScene,
            golemComplete = golemComplete,
            kongComplete = kongComplete,
            pacComplete = pacComplete,
            magicianDisappeared = magicianDisappeared,
            metAduoForp = metAduoForp,
            pongComplete = pongComplete,
            numOfFood = numOfFood
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public static void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            print("loading from file...");

            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            PlayerAttributes.InitializeAttributes();

            // Assigns the attributes
            PlayerAttributes.GlobalGameState = saveObject.gameState;
            PlayerAttributes.CurrentScene = saveObject.scene;
            PlayerAttributes.PlayerLevel = saveObject.level;
            PlayerAttributes.PlayerXP = saveObject.xp;
            PlayerAttributes.PlayerHealth = saveObject.playerHealth;
            PlayerAttributes.PlayerSpeed = saveObject.playerSpeed;
            PlayerAttributes.PlayerDefence = saveObject.playerDefence;
            PlayerAttributes.PlayerDamage = saveObject.playerDamage;
            PlayerAttributes.PlayerColor = saveObject.playerColor;
            PlayerAttributes.Alive = saveObject.playerAlive;

            PlayerAttributes.StartScene = saveObject.startScene;
            PlayerAttributes.GolemComplete = saveObject.golemComplete;
            PlayerAttributes.KongComplete = saveObject.kongComplete;
            PlayerAttributes.PacComplete = saveObject.pacComplete;
            PlayerAttributes.MagicianDisappeared = saveObject.magicianDisappeared;
            PlayerAttributes.MetAduoForp = saveObject.metAduoForp;
            PlayerAttributes.PongComplete = saveObject.pongComplete;

            PlayerAttributes.NumOfFood = saveObject.numOfFood;

            PlayerAttributes.Alive = true;
            PlayerAttributes.PlayerHealth = 100;
        }
        else
        {
            print("loading from defaults");
            PlayerAttributes.InitializeAttributes();
        }
    }
}