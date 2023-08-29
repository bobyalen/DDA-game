using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthDrop : ScriptableObject
{ 
    public GameObject sprite;
    public string lootName;
    public int dropRate;


    public HealthDrop(string lootName, int dropRate)
    {
        this.lootName = lootName;
        this.dropRate = dropRate;
    }
}
