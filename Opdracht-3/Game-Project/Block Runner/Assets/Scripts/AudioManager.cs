using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;
    public AudioSource sfxSource;

    public AudioClip jumpClip;
    public AudioClip explosionClip;
    public AudioClip gameOverClip;
    public AudioClip victoryClip;

    void Awake()
    {
        if (I==null) { I=this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    } // Ensures this instance persists across scenes

    public void PlayJump()      => sfxSource.PlayOneShot(jumpClip); // Plays sound effect for jumping
    public void PlayExplosion() => sfxSource.PlayOneShot(explosionClip); // Plays sound effect for explosion
    public void PlayGameOver()  => sfxSource.PlayOneShot(gameOverClip); // Playx sound effect for game over
    public void PlayVictory()   => sfxSource.PlayOneShot(victoryClip); // Plays sound effect for victory
}
// This script manages audio playback for game events, such as jumping, death, game over, and victory.