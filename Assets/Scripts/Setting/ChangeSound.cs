using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSound : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> sprites;

    private int active;

    private void Start()
    {
        active = PlayerPrefs.GetInt("Sound", 0);
        ChangeSoundButton(active);
    }

    public void PressStartStopMusic()
    {
        if (active == 1)
        {
            active = 0;
            ChangeSoundButton(0);
        }
        else
        {
            active = 1;
            ChangeSoundButton(1);
        }

    }
    public void ChangeSoundButton(int newSound)
    {
        PlayerPrefs.SetInt("Sound", newSound);
        image.sprite = sprites[newSound];
    }
}
