using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorController : MonoBehaviour
{
    public GameObject predators;
    public Transform bearSpawn;
    public List<Enemy> enemies= new List<Enemy>();
    List<GameObject> Tospawn = new List<GameObject>();
    int x, z;
    public int enemyNum;
    public int enemyCost;
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
            Instantiate(Tospawn[0], newSpawn, Quaternion.identity);
            Tospawn.RemoveAt(0);
            enemyNum++;
        }
    }
    IEnumerator spawn()
    {
        while (Tospawn.Count > 0)
        {
            Vector3 newSpawn = new Vector3(bearSpawn.position.x + Random.Range(-10, 50), 10, bearSpawn.position.z + Random.Range(-10, 50));

            Instantiate(Tospawn[0], newSpawn, Quaternion.identity);
            Tospawn.RemoveAt(0);
            yield return new WaitForSeconds(1);
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
            if (enemyCost-cost >= 0) 
            {
                Tospawn.Add(enemies[enemyType].enemyBase.predators);
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

}
