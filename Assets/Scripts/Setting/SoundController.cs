using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;

    [SerializeField] private AudioSource audioSource;

    private int sound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        sound = PlayerPrefs.GetInt("Sound", 0);
        ChangeSound(sound);
    }

    public void ChangeSound(int newSound)
    {
        sound = newSound;
        PlayerPrefs.SetInt("Sound", sound);
        if (sound == 0)
        {
            audioSource.mute = true;
        }
        else if (sound == 1)
        {
            audioSource.mute = false;
        }
    }

    private void Update()
    {
        sound = PlayerPrefs.GetInt("Sound", 0);
        if (sound == 0)
        {
            audioSource.mute = true;
        }
        else if (sound == 1)
        {
            audioSource.mute = false;
        }
    }
}
