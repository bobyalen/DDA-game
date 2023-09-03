using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float remaingTime;
    bool showTimer;
    public TMP_Text timerText;
    void Start()
    {
        showTimer= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (showTimer)
        {
            if (remaingTime > 0)
            {
                remaingTime+= Time.deltaTime;
            }
            else
            {
                showTimer= false;
            }
        }
    }

    public float getTimer()
    { return remaingTime; }
}
