using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PredatorController : MonoBehaviour
{
    public GameObject predators;
    [SerializeField]
    public PlayerModel playerStats;
    [SerializeField]
    DDAControl DDA;
    public Transform bearSpawn;
    float timer = 0f;
    public List<Enemy> enemies= new List<Enemy>();
    public List<Enemy> SpawnObjects = new List<Enemy>();
    List<GameObject> Tospawn = new List<GameObject>();
    int x, z;
    public int enemyNum;
    public int maxEnemies;
    int wave;
    int avgDMG;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnEnemy());
        //EnemyCost();
        Reset();
        maxEnemies = 12;
        timer= 14;
    }

    public void Reset()
    {
        wave = 1;
    }

    public void enemyKilled()
    {
        enemyNum--;
        playerStats.updateKills();
    }
    void Update()
    {
        if (timer <= 15)
        {
            timer += Time.deltaTime;
        }
        else
        {

            DDA.setDiff(wave);
            avgDMG = 0;
            EnemyWaves();
            playerStats.resetAccuracy();
            while (Tospawn.Count > 0)
            {
                Vector3 newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-20, 30), bearSpawn.position.y, bearSpawn.position.z + Random.Range(-20, 30));
                while (Vector3.Distance(bearSpawn.position, newSpawn) <= 15)
                {
                    newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-20, 30), bearSpawn.position.y, bearSpawn.position.z + Random.Range(-20, 30));
                }
                //Instantiate(Tospawn[0], newSpawn, Quaternion.identity);
                //DDA.setDiff();
                Instantiate(SpawnObjects[0].enemyBase.predators, newSpawn, Quaternion.identity);

                setStats(SpawnObjects[0]);
                //setSpeed(SpawnObjects[0]);
                Tospawn.RemoveAt(0);
            }
            timer= 0;
            playerStats.updatMaxHits();
        }
    }

    public void EnemyWaves()
    {
        int enemyType = Random.Range(0,enemies.Count);
        //number of enemies to spawn
        int spawnNum = Mathf.Min((wave - 1) + 5, maxEnemies);
        Enemy newspawn = enemies[enemyType];
        for (int i = 0;i<spawnNum;i++)
        { 
            Tospawn.Add(enemies[enemyType].enemyBase.predators);
            SpawnObjects.Add(newspawn);
        }
        wave++;
        enemyNum += SpawnObjects.Count;
    }

    [System.Serializable]
    public class Enemy
    {
       public Enemies enemyBase;
    }

    #region enemyStats
    //Change predator HP (used for DDA)
    void setStats(Enemy enemy)
    {
        enemy.enemyBase.baseHP= DDA.GetHP();
        enemy.enemyBase.predators.GetComponent<DamageHandler>().setHealth(enemy.enemyBase.baseHP);
        enemy.enemyBase.speed= DDA.GetSpeed(enemy.enemyBase.baseHP);
        enemy.enemyBase.predators.GetComponent<Bear>().setSpeed(enemy.enemyBase.speed);
        enemy.enemyBase.baseAttack= DDA.GetDmg(enemy.enemyBase.baseHP);
        enemy.enemyBase.predators.GetComponent<Bear>().setDMG(enemy.enemyBase.baseAttack);
        enemy.enemyBase.baseRange= DDA.GetAggro();
        enemy.enemyBase.predators.GetComponent<Bear>().setAgro(enemy.enemyBase.baseRange);
        avgDMG += enemy.enemyBase.baseAttack;
    }
    public int getavgDMG()
    {
        return avgDMG/enemyNum;
    }
    #endregion
}
