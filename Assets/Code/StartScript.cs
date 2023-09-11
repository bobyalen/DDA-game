using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static StartScript Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

}
