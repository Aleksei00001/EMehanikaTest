using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    private void Start()
    {
        left.transform.position = new Vector2(-(((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize + (left.transform.localScale.x / 2)), 0);
        right.transform.position = new Vector2((((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize + (right.transform.localScale.x / 2)), 0);
    }
}
