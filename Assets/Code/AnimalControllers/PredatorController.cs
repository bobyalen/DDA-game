using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PredatorController;

public class PredatorController : MonoBehaviour
{
    public GameObject predators;
    [SerializeField]
    public PlayerModel playerStats;
    [SerializeField]
    DDAControl DDA;
    public Transform bearSpawn;

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
        wave= 1;
        maxEnemies = 12;
    }

    /*
    IEnumerator spawnEnemy()
    {
        while (enemyNum < 10)
        {
            Vector3 newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-10, 50), 10, bearSpawn.position.z + Random.Range(-10, 50));

            Instantiate(predators, newSpawn,Quaternion.identity);
            yield return new WaitForSeconds(1);
            enemyNum++;
        }
    }
    */
    public void enemyKilled()
    {
        enemyNum--;
        playerStats.updateKills();
    }
    void Update()
    {
        if (enemyNum <=0)
        {
            DDA.setDiff();
            EnemyWaves();
            playerStats.resetAccuracy();
        }
        if (Tospawn.Count > 0)
        {
            Vector3 newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-20, 30), bearSpawn.position.y, bearSpawn.position.z + Random.Range(-20, 30));
            while (Vector3.Distance(bearSpawn.position,newSpawn) <=15)
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
        enemyNum = SpawnObjects.Count;
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
    }

    /*
    public void setHealth(Enemy enemy)
    {
        enemy.enemyBase.baseHP = (int)DDA.GetHP();
        enemy.enemyBase.predators.GetComponent<DamageHandler>().setHealth(enemy.enemyBase.baseHP);
    }
    
    //Change predator speed (used for DDA)
    public void setSpeed(Enemy enemy)
    {
        enemy.enemyBase.speed = (int)DDA.GetSpeed();
        enemy.enemyBase.predators.GetComponent<Bear>().setSpeed(enemy.enemyBase.speed);
    }
    public void setAttack(Enemy enemy)
    {
        enemy.enemyBase.baseAttack = (int)DDA.GetDmg();
        avgDMG += enemy.enemyBase.baseAttack;
        enemy.enemyBase.predators.GetComponent<Bear>().setDMG(enemy.enemyBase.baseAttack);
    }
    */
    public int getavgDMG()
    {
        return avgDMG/enemyNum;
    }
    #endregion
}
