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
    }

    public void PlayJump()      => sfxSource.PlayOneShot(jumpClip);
    public void PlayExplosion() => sfxSource.PlayOneShot(explosionClip);
    public void PlayGameOver()  => sfxSource.PlayOneShot(gameOverClip);
    public void PlayVictory()   => sfxSource.PlayOneShot(victoryClip);
}
// This script manages audio playback for game events, such as jumping, death, game over, and victory.