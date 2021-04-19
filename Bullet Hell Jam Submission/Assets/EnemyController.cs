using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITakeDamage
{
    public int EnemyHealth { get; private set; }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(gameObject))
            return;
        EnemyHealth -= amount;
        Debug.Log("Enemy Health: " + EnemyHealth);
        if (EnemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
