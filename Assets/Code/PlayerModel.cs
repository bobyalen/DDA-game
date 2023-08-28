using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

//All player data for DDA
public class PlayerModel : MonoBehaviour
{       
    int shots;
    int shotsHit;
    int hitCounter;
    int kills;
    float HP;
    float Points;
    float hitTimer;
    float resetTimer = 5.0f;
    int deaths;

    
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        shots = 0;
        shotsHit= 0;
        hitCounter= 0;
        kills = 0;
        Points = 0;
        hitTimer = 0;
        deaths = 0;
    }

    public void updateKills()
    {
        kills++;
    }public void updateHits()
    {
        hitCounter++;
    }

    float avgHits()
    {
        return (float)hitTimer/hitCounter;
    }

    public int killCounter()
    {
        return kills;
    }
    public int hitCount()
    {
        return hitCounter;
    }
    // Update is called once per frame
    void Update()
    {
        hitTimer += Time.deltaTime;
        if (hitTimer >= resetTimer)
        {
            Debug.Log("AVG Hits: " + avgHits());
            hitTimer= 0;
            hitCounter= 0;        }
    }

    //called everytime player shoots
    public void updateShots(bool hit)
    {
        shots++;
        if(hit)
        {
            shotsHit++;
        }

    }
    public float CalculateAccuracy()
    {
        if (shots == 0)
            return 0f;

        return ((float)shotsHit/shots)*100;
    }

    public void updateDeath()
    {
        deaths++;
    }

    public int getShots() { return shots;}
    public int getShotsHit() { return shotsHit; }
}
