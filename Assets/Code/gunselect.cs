using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunselect : MonoBehaviour
{
    // Start is called before the first frame update
    public static gunselect Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
