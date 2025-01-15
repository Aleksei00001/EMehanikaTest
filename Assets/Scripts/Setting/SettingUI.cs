using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    private bool active;
    public void PressOpenCloseSetting()
    {
        if (active == true)
        {
            active = false;
            settings.SetActive(active);
        }
        else
        {
            active = true;
            settings.SetActive(active);
        }

    }

}
