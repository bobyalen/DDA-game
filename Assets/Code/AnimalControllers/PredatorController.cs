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
    public int enemyCost;
    int avgDMG;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnEnemy());
        //EnemyCost();
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
            EnemyCost();
        }
        if (Tospawn.Count > 0)
        {
            Vector3 newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-10, 50), bearSpawn.position.y, bearSpawn.position.z + Random.Range(-10, 50));
            //Instantiate(Tospawn[0], newSpawn, Quaternion.identity);
            DDA.setDiff();
            Instantiate(SpawnObjects[0].enemyBase.predators, newSpawn, Quaternion.identity);

            setHealth(SpawnObjects[0]);
            setSpeed(SpawnObjects[0]);
            Tospawn.RemoveAt(0);
            enemyNum++;
        }
    }


    public void EnemyCost()
    {
        enemyCost= 10;
        EnemyWaves();
    }

    public void EnemyWaves()
    {
        while (enemyCost > 0)
        {
            int enemyType = Random.Range(0,enemies.Count);
            int cost = enemies[enemyType].cost;
            Enemy newspawn = enemies[enemyType];
            
            if (enemyCost-cost >= 0) 
            {
                Tospawn.Add(enemies[enemyType].enemyBase.predators);
                SpawnObjects.Add(newspawn);
                enemyCost -= cost;
            }
            else if(enemyCost<=0)
            {
                break;
            }
        }
    }

    [System.Serializable]
    public class Enemy
    {
       public Enemies enemyBase;
       public int cost;
    }

    #region enemyStats
    //Change predator HP (used for DDA)
    public void setHealth(Enemy enemy)
    {

        //enemy.enemyBase.baseHP =Random.Range(100, 300);
        enemy.enemyBase.baseHP = (int)DDA.GetStats().HP;
        enemy.enemyBase.predators.GetComponent<DamageHandler>().setHealth(enemy.enemyBase.baseHP);
    }
    
    //Change predator speed (used for DDA)
    public void setSpeed(Enemy enemy)
    {
        enemy.enemyBase.speed = (int)DDA.GetStats().speed;
        enemy.enemyBase.predators.GetComponent<Bear>().setSpeed(enemy.enemyBase.speed);
    }
    public void setAttack(Enemy enemy)
    {
        enemy.enemyBase.baseAttack = (int)DDA.GetStats().damage;
        avgDMG += enemy.enemyBase.baseAttack;
        enemy.enemyBase.predators.GetComponent<Bear>().setDMG(enemy.enemyBase.baseAttack);
    }

    public int getavgDMG()
    {
        return avgDMG/enemyNum;
    }
    #endregion
}
