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

    [SerializeField] private GameObject explosionPrefab;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacles")
        {
            // If the player collides with an obstacle, play death sound and game over sound:
            AudioManager.I.PlayExplosion();
            AudioManager.I.PlayGameOver();
            // Spawns the explosion effect at the contact point:
            Vector3 spawnPos = other.contacts[0].point;
            Instantiate(explosionPrefab, spawnPos, Quaternion.identity);

            gameObject.SetActive(false); // Deactivates the player GameObject

            GameManager.Instance.GameOver(); // Calls the GameOver method from GameManager to handle game state
        }
    }
}
// this script handles player collision with obstacles in a 2D game.