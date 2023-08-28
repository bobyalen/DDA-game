using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    public float health;
    int score;
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public TMP_Text final;
    public float remaingTime;
    bool showTimer = true;
    public TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + health.ToString();
        scoreText.text = "Score" + score.ToString();
        int time = (int)remaingTime;
        timerText.text = time.ToString();
        if (showTimer)
        {
            if (remaingTime > 0)
            {
                remaingTime -= Time.deltaTime;
            }
            else
            {
                showTimer = false;
                Debug.Log("Time up");
            }
        }
        end();
    }

    public void end()
    {
        if (health <= 0 || remaingTime <= 0)
        {
            Time.timeScale = 0;
            finalScreen.Instance.Show();
            final.text = "Game Over final Score: " + score.ToString();
        }
    }
    
    public float getScore()
    {
        return score;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        this.GetComponent<PlayerModel>().updateHits();
        if (health <= 0)
        {
            Invoke("Destroy", 2.8f);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bearattack")
        {
            Debug.Log("-15hp");
            TakeDamage(15f);
        }
    }


    public void addScore()
    {
        score+=150;
    }

}
