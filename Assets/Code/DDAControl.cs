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

        timeSurvived = 0;
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

    public void ResetAll()
    {
        timeSurvived = 0;
        playerStats.Reset();
        player.Reset();
        GameObject.Find("Predators").GetComponent<PredatorController>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //time game  has ben played for 
        timeSurvived += Time.deltaTime;
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
    public int GetAggro()
    {
        return (int)Random.Range(currentStats.aggroRange.x, currentStats.aggroRange.y);
    }

        float calculateScore()
    {
        float avgScore = ((float)killTime()*0.45f) + ((float)player.CalculateAccuracy()*0.2f) + ((float)player.avgHits()*0.35f);
        Debug.Log(avgScore);
        return avgScore;
    }

    float killTime()
    {
        //time to kill
        float worstTTK = 11f;
        float bestTTK = 5.0f;
        float TTK = timeSurvived / (float)player.killCounter();
        TTK = Mathf.Clamp(TTK, bestTTK, worstTTK);
        return (1.0f-(TTK-bestTTK)/(worstTTK/bestTTK))*100f;

    }

    public void setDiff(int wave)
    {
        if (wave > 1)
        {
            float playerScore = calculateScore();
            if (playerScore <= 25f)
            {
                DecreaseDiff();
            }
            if (playerScore >= 75f)
            {
                IncreaseDiff();
            }
        }
        currentStats = enemyDiff();
    }

    void DecreaseDiff()
    {
        if (currentDifficulty > difficulty.Beginner)
        {
            currentDifficulty -= 1;
            Debug.Log("Increased difficulty to: " + currentDifficulty.ToString());
        }
    }

    void IncreaseDiff()
    {
        if (currentDifficulty < difficulty.Master)
        {
            currentDifficulty += 1;
            Debug.Log("Increased difficulty to: " + currentDifficulty.ToString());
        }
    }
    

    difficultyStats enemyDiff()
    {
        PlayerPrefs.SetString("Difficulty", currentDifficulty.ToString());
        PlayerPrefs.Save();
        switch (currentDifficulty)
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


    public Vector2 diffScore()
    {
        switch (currentDifficulty)
        {
            case difficulty.Beginner:
                return new Vector2(3,100);
            case difficulty.Easy:
                return new Vector2(5,125);
            case difficulty.Normal:
                return new Vector2(8, 150);
            case difficulty.Hard:
                return new Vector2(10, 175);
            case difficulty.Master:
                return new Vector2(12, 200);
        }
        return new Vector2(8, 150);
    }

    public int dropChance()
    {
        switch (currentDifficulty)
        {
            case difficulty.Beginner:
                return 30;
            case difficulty.Easy:
                return 25;
            case difficulty.Normal:
                return 20;
            case difficulty.Hard:
                return 15;
            case difficulty.Master:
                return 12;
        }
        return 20;
    }
}
