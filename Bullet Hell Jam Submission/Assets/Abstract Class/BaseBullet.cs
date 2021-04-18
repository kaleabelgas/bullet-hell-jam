using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected int damageAmount;
    public virtual void SetDirection(Vector2 direction, float speed)
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ITakeDamage toDamage = other.GetComponent<ITakeDamage>();

        if (toDamage != null)
        {
            toDamage.GetDamaged(damageAmount);
            gameObject.SetActive(false);
        }
    }
}
