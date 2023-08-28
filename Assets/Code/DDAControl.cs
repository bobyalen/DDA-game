using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

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
        public float speed;
        public float HP;
        public float damage;
        public float aggroRange;
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
            Debug.Log("Kill between kills: " + calculateScore());
        }
    }

    public difficulty GetDifficulty()
    {
        return currentDifficulty;
    }

    float calculateScore()
    {
        float avgScore = 0;
        float score = 0;
        avgScore += player.CalculateAccuracy();//killTime();
        return avgScore;
    }

    int killTime()
    {
        //time to kill
        float worstTTK = 15.0f;
        float TTK = timeSurvived / (float)player.killCounter();
        float normalTTK = Mathf.Clamp01(TTK / worstTTK);
        int score = Mathf.RoundToInt((1.0f - normalTTK) * 100);
        return score;

    }

    public void setDiff()
    {
        float playerScore = calculateScore();
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
        if (playerScore <= 0.7f)
        {
            currentDifficulty = difficulty.Hard;
        }
        else
        {
            currentDifficulty = difficulty.Master;
        }

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



        void setHealth(float baseHP)
    {
        float health = baseHP;
    }



}
