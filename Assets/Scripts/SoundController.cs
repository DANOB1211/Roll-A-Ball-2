using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //Set audiosources audioclip to be passed in sound
        audioSource.clip = _newSound;
        //Play audiosource
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //check to see if collided object has an audio source.
        //This is a failsafe in case you didn't attach to wall.
        if (_go.GetComponent<AudioSource>() != null)
        {
            //Play audio on wall object
            _go.GetComponent<AudioSource>().Play();
        }
    }
}
