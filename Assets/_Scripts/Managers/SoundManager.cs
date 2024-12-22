using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip driveSound;
    public AudioClip highScoreMusic;
    public AudioClip duffedShot;

    public void MenuMusic()
    {
        audioSource.clip = menuMusic; // Assign the music clip to the AudioSource
        audioSource.loop = true;
        audioSource.Play(); // Play the assigned clip
    }
    public void GameMusic()
    {
        audioSource.clip = gameMusic; // Assign the music clip to the AudioSource
        audioSource.loop = true;
        audioSource.Play(); // Play the assigned clip
    }

    public void PlayDriveSound()
    {
        audioSource.PlayOneShot(driveSound);
    }


    public void PlayHighScoreMusic()
    {
        audioSource.PlayOneShot(highScoreMusic);
    }

    public void PlayDuffedShot()
    {
        audioSource.PlayOneShot(duffedShot);
    }


}
