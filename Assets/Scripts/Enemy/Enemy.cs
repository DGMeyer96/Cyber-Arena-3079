using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;

    public void TakeDamage(float damageTaken)
    {
        Health -= damageTaken;

        if(Health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
