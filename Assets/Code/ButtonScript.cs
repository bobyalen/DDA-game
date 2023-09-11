using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void pressed()
    {
        Debug.Log("You have clicked the button!");
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
