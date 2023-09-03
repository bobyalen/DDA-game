using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class DDAControl : MonoBehaviour
{
    public enum difficulty
    {
        Beginner,
        Easy,
        Normal,
        Hard,
        Master
    }

    [System.Serializable]
    public class difficultyStats
    {
        public Vector2 speed;
        public Vector2 HP;
        public Vector2 damage;
        public Vector2 aggroRange;
    }

    public difficultyStats BeginnerStats;
    public difficultyStats easyStats;
    public difficultyStats normalStats;
    public difficultyStats hardStats;
    public difficultyStats masterStats;
    difficultyStats currentStats;

    int damage;
    int health;
    int spawnRate;
    float combatScore;
    float timeSurvived;
    [SerializeField]
    PlayerModel player;
    [SerializeField]
    playerController playerStats;
    [SerializeField]
    PredatorController.Enemy enemy;
    public difficulty currentDifficulty;
    // Start is called before the first frame update
    void Start()
    {
        //enemy= GameObject.Find("Predators").GetComponent<PredatorController.Enemy>();
        Reset();
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            string difficultyString = PlayerPrefs.GetString("Difficulty");
            if (Enum.TryParse(difficultyString, out difficulty diff))
            {
                currentDifficulty = diff;
            }
        }
        else
        {
            currentDifficulty = difficulty.Normal;
        }
            currentStats = enemyDiff();
    }

    public difficultyStats GetStats()
    {
        return currentStats;
    }

    void Reset()
    {
        timeSurvived = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //time game  has ben played for 
        timeSurvived += Time.deltaTime;
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("Time between kills: " + killTime());
        }
    }

    public difficulty GetDifficulty()
    {
       PlayerPrefs.SetString("Difficulty", currentDifficulty.ToString());
        PlayerPrefs.Save();
        return currentDifficulty;
    }
   
    public int GetHP()
    {
        return (int)Random.Range(currentStats.HP.x, currentStats.HP.y);
    }
    
    public int GetSpeed(int HP)
    {
        if (currentDifficulty == difficulty.Normal || currentDifficulty == difficulty.Hard || currentDifficulty == difficulty.Master)
        {
            if ( HP >= (currentStats.HP.x+ (currentStats.HP.y- currentStats.HP.x)*0.8f))
            {
                return (int)Random.Range(currentStats.speed.x,(currentStats.speed.y -currentStats.speed.x)*0.25f);
            }
            else if (HP <= (currentStats.HP.x + (currentStats.HP.y - currentStats.HP.x) * 0.25f))
            {
                return (int)Random.Range(currentStats.speed.x + (currentStats.speed.y - currentStats.speed.x)*0.8f, currentStats.speed.y);
            }
        }
        return (int)Random.Range(currentStats.speed.x, currentStats.speed.y);
    }

    public int GetDmg(int HP)
    {
        if (currentDifficulty == difficulty.Normal || currentDifficulty == difficulty.Hard || currentDifficulty == difficulty.Master)
        {
            if (HP >= (currentStats.HP.x + (currentStats.HP.y - currentStats.HP.x) * 0.8f))
            {
                return (int)Random.Range(currentStats.damage.x, (currentStats.damage.y - currentStats.damage.x) * 0.25f);
            }
            else if (HP <= (currentStats.HP.x + (currentStats.HP.y - currentStats.HP.x) * 0.25f))
            {
                return (int)Random.Range(currentStats.damage.x + (currentStats.damage.y - currentStats.damage.x) * 0.8f, currentStats.damage.y);
            }
        }
        return (int)Random.Range(currentStats.damage.x, currentStats.damage.y);
    }

    float calculateScore()
    {
        float avgScore = ((float)killTime()*0.45f) + ((float)player.CalculateAccuracy()*0.25f) + ((float)player.avgHits()*0.35f);
        return avgScore;
    }

    float killTime()
    {
        //time to kill
        float worstTTK = 15.0f;
        float bestTTK = 8.0f;
        float TTK = timeSurvived / (float)player.killCounter();
        TTK = Mathf.Clamp(TTK, bestTTK, worstTTK);
        return (1.0f-(TTK-bestTTK)/(worstTTK/bestTTK))*100f;

    }

    public void setDiff()
    {
        float playerScore = calculateScore();
        Debug.Log("Performance Score: " + playerScore);
        if (playerScore <= 0.3f)
        {
            currentDifficulty = difficulty.Beginner;
        }
        if (playerScore <= 0.45f)
        {
            currentDifficulty = difficulty.Easy;
        }
        if (playerScore <= 0.6f)
        {
            currentDifficulty = difficulty.Normal;
        }
        if (playerScore <= 0.75f)
        {
            currentDifficulty = difficulty.Hard;
        }
        else
        {
            currentDifficulty = difficulty.Master;
        }
        currentStats = enemyDiff();
    }



    difficultyStats enemyDiff()
    {
        switch(currentDifficulty)
        {
            case difficulty.Beginner:
                return BeginnerStats; 
            case difficulty.Easy:
                return easyStats;
            case difficulty.Normal:
                return normalStats;
            case difficulty.Hard:
                return hardStats;
            case difficulty.Master:
                return masterStats;
        }
        return normalStats;
    }


    public int diffScore()
    {
        switch (currentDifficulty)
        {
            case difficulty.Beginner:
                return 3;
            case difficulty.Easy:
                return 5;
            case difficulty.Normal:
                return 8;
            case difficulty.Hard:
                return 10;
            case difficulty.Master:
                return 12;
        }
        return 8;
    }
}
