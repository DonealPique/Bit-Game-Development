using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.onplay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true); // Activates the player when the game starts
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacles")
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver(); // Calls the GameOver method from GameManager to handle game state
        }
    }
}
// this script handles player collision with obstacles in a 2D game.