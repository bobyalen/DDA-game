using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class Enemies : ScriptableObject
{
    public string prefabName;
    public GameObject predators;
    public int baseAttack;
    public int baseRange;
    public int baseHP;
    public int spawnRange;
    public float speed;
}