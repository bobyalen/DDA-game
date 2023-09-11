using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField]
    int healAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController playerHealth = other.GetComponent<playerController>();
            if (playerHealth.health < playerHealth.maxHealth)
            {
                playerHealth.heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
