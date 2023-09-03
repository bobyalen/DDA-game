using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    public float health;
    int score;
    Vector2 diffScoring;
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
        if (showTimer)
        {
            InvokeRepeating("diffScore", 1f, 1f);
        }
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
                remaingTime += Time.deltaTime;
            }
            else
            {
                showTimer = false;
            }
        }
        end();
    }

    public void end()
    {
        if (health <= 0)
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
            /*
            if (other.GetComponentInParent<Bear>().attackReset)
            {
                TakeDamage(other.GetComponentInParent<Bear>().dmg);
            }
            */
            TakeDamage(other.GetComponentInParent<Bear>().dmg);

        }
    }

    void diffScore()
    {
        diffScoring = GameObject.Find("DDAController").GetComponent<DDAControl>().diffScore();
        score += (int)diffScoring.x;
    }


    public void addScore()
    {
        score+= (int)diffScoring.y;
    }


    public void heal(int amount)
    {
        if (health + amount <= maxHealth)
        {
            health += amount;
        }
        else health = maxHealth;
    }

}
