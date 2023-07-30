using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {

        health = maxHealth;
    }

    public void Damage(float damage)
    {
        Debug.Log("Getting Hit"+ damage);
        health-=damage;
    }

    public float getHealth()
    {
        return health;
    }
}
