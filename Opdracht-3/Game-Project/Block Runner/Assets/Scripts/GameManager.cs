using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour

{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion
    public float currentScore = 0f; // Current score of the game

    public SaveData Data; // Data object to hold game data

    public bool isPlaying = false;

    public UnityEvent onplay = new UnityEvent(); // Event to trigger when the game starts
    public UnityEvent onGameOver = new UnityEvent(); // Event to trigger when the game ends

    private void Start()
    {
        string loadedData = SaveSystem.Load("save"); // Load saved data
        if (loadedData != null)
        {
            Data = JsonUtility.FromJson<SaveData>(loadedData);
        }
        else
        {
            Data = new SaveData();
        }
    }

      [Header("Victory")]
    public float victoryScore = 10f;           // how many points to win
    public UnityEvent onVictory = new UnityEvent();

    private void Update()
   {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
            if (currentScore >= victoryScore)
            {
                WinGame();
                AudioManager.I.PlayVictory(); // Play victory sound
            }
        }
    }

    private void WinGame()
    {
        isPlaying = false;          // stop the gameplay loop
        onVictory.Invoke();         // fire your victory event
    }

    public void StartGame()
    {
        onplay.Invoke();
        isPlaying = true; // Start the game
        currentScore = 0;
    }
    public void GameOver()
    {
        if (Data.highscore < currentScore)
        {
            Data.highscore = currentScore; // Update high score if current score is higher
            string saveString = JsonUtility.ToJson(Data);
            SaveSystem.Save("save", saveString); // Save the new high score
        }
        isPlaying = false; // Stop the game
        onGameOver.Invoke();
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(Data.highscore).ToString();
    }

}

// This script manages the game state, including score tracking and game over conditions.