using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected int damageAmount;
    public GameObject Owner;
    public virtual void SetDirection(Vector2 direction, float speed)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ITakeDamage toDamage = other.GetComponent<ITakeDamage>();
        Debug.Log("Hit " + other);

        if (toDamage != null)
        {
            toDamage.GetDamaged(damageAmount, Owner);            
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        Owner = gameObject;
    }
}
