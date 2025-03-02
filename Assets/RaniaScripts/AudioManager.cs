using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("audio source")]
    [SerializeField] AudioSource music_source;
    [SerializeField] AudioSource sfx_source;


    [Header("audio clip")]
    public AudioClip background;
    public AudioClip button_sound;

    public void Start()
    {
        music_source.clip = background;
        music_source.Play();
    }
}
