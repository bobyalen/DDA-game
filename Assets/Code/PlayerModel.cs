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
    int dmg;
    int maxHits;
    [SerializeField]
    playerController player;
    [SerializeField]
    PredatorController predator;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        shots = 0;
        shotsHit= 0;
        hitCounter= 0;
        kills = 0;
        Points = 0;
        hitTimer = 0;
        dmg = 0;
        maxHits = 0;
    }

    public void updateKills()
    {
        kills++;
    }public void updateHits()
    {
        hitCounter++;
    }

    public void updatMaxHits()
    {
        maxHits = (int)player.health/predator.getavgDMG();
    }

    public void playerHealed(int amount)
    {
        if (amount > dmg)
        {
            if (player.health+amount >=player.maxHealth)
            {
                amount= Mathf.RoundToInt(player.maxHealth-player.health);
            }
            maxHits += Mathf.RoundToInt(amount / dmg);
        }
    }

    public int killCounter()
    {
        return kills;
    }
    public int hitCount()
    {
        return hitCounter;
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


    #region calculations

    public int getShots() { return shots;}
    public int getShotsHit() { return shotsHit; }

    public void resetAccuracy()
    {
        shots= 0;
        shotsHit= 0;
        hitCounter= 0;
    }



    //player accuracy and amount of times hit
    public float CalculateAccuracy()
    {
        if (shots == 0)
            return 0f;
        float acc = ((float)shotsHit/(float)shots)*100;
        return (int)acc ;
    } 

    public float avgHits()
    {
        if (hitCounter == 0)
            return 100f;
        else if (hitCounter >= maxHits)
            return 0f;
        float avghitscore = Mathf.Clamp01(1f - ((float)hitCounter / (float)maxHits));
        return avghitscore*100f;
    }
    #endregion
}
