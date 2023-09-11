using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    // Start is called before the first frame update
    public List<HealthDrop> drops = new List<HealthDrop>();

    HealthDrop getDrop()
    {
        int dropChance = Random.Range(1, 101);
        List<HealthDrop> dropList = new List<HealthDrop>();
        foreach (HealthDrop drop in drops)
        {
            if (dropChance <= (drop.dropRate = GameObject.Find("DDAController").GetComponent<DDAControl>().dropChance()))
            {
                dropList.Add(drop);
            }
        }
        if (dropList.Count > 0)
        {
           HealthDrop dropped = dropList[Random.Range(0,dropList.Count)];
            return dropped;
        }

        return null;
    }


    public void dropLoot(Vector3 pos)
    {
        HealthDrop drop = getDrop();
        if (drop != null)
        {
            GameObject Loot = Instantiate(drop.sprite, pos, Quaternion.identity);

        }
    }
}
