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

        if (toDamage != null)
        {
            AudioManager.instance.Play("hit");
            toDamage.GetDamaged(damageAmount, Owner);
            //if (other.gameObject.CompareTag("Player"))
            //{
            //}
                gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        ObjectPooler.Instance.SpawnFromPool("hit effect", transform.position, transform.rotation);
        Owner = gameObject;
    }
}
