using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthAmount = 50f;

    public void TakeDamage(float damageTaken)
    {
        HealthAmount -= damageTaken;
        Debug.Log("Health is : " + HealthAmount);
        if (HealthAmount <= 0)
        {
            Death();
        }
    }
    //TODO I need to have it respawn rather than delete self
    void Death()
    {
        Destroy(gameObject);
    }
}
