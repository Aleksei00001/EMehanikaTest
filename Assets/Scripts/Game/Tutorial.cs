using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int step;

    [SerializeField] private List<GameObject> stepMenu;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            stepMenu[0].SetActive(true);
        }
    }

    public void CloseStepAndOpenNext()
    {
        stepMenu[step].SetActive(false);
        step++;
        if (step < stepMenu.Count)
        {
            stepMenu[step].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }
}
