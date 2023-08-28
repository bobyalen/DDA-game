using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalScreen : MonoBehaviour
{
    public static finalScreen Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        Hide();
    }

    // Update is called once per frame
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
