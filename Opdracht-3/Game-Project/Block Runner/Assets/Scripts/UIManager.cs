using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject StartmenuUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI; // UI element to display high score
    [SerializeField] private TextMeshProUGUI gameOverScoreUI; // UI element to display current score

      [Header("Victory UI")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button victoryRestartButton;

    GameManager gm;
    private void Start()
     {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);
        gm.onVictory.AddListener(ActivateVictoryUI);
    } // Initializes the UIManager, setting up listeners for game over and victory events from GameManager.

    private void ActivateVictoryUI()
    {
        victoryPanel.SetActive(true);
        scoreUI.gameObject.SetActive(false);
    } // Activates the victory UI when the game is won, hiding the score UI.

    public void VictoryRestartHandler()
    {

        victoryPanel.SetActive(false);
        gm.StartGame();
        scoreUI.gameObject.SetActive(true);
    } // Handles the restart button click in the victory panel, hiding the victory UI and starting a new game.
    public void PlayButtonHandler()
    {
        gm.StartGame();
    } // Handles the play button click, starting the game by calling the StartGame method on GameManager.

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true); // Activates the game over UI when the game ends

        gameOverScoreUI.text = "Score: " + gm.PrettyScore(); // Updates the score UI with the current score
        gameOverHighScoreUI.text = "HighScore: " + gm.PrettyHighscore(); // Updates the high score UI
    }
    private void OnGUI()
    {
        scoreUI.text = GameManager.Instance.PrettyScore(); // Updates the score UI with the current score from GameManager
    }
}

// This script manages the UI, specifically updating the score display in a 2D game.