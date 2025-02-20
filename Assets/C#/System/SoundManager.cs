using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;//строка для того чтобы обращаться к менеджеру звуков из любого места

    public AudioClip PistolSound;

    public AudioClip BaseballBetSound;

    public AudioClip KnifeSound;

    public AudioClip ButtonSound;

    public AudioClip CoinSound;

    public AudioClip StatusSound; //звук при сборе бустера

    public AudioClip HitSound; //звук при ударе об стену 

    public AudioClip LeftRightSound;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PressButtonSound()
    {
        if (audioSource != null && ButtonSound != null)
        {
            audioSource.PlayOneShot(ButtonSound);
        }
    }
}
