using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected int damageAmount;

    protected int _damageAmount;
    private GameObject Owner;

    protected virtual void OnEnable()
    {
        _damageAmount = damageAmount;
    }

    public virtual void SetDirection(Vector2 direction, float speed, GameObject owner)
    {
        Owner = owner;
        //Debug.Log($"Setting {Owner.name} to {owner.name}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ITakeDamage toDamage = other.GetComponent<ITakeDamage>();

        if (toDamage != null)
        {
            //AudioManager.instance.Play("hit");
            //Debug.Log(Owner.name + "is shooting");
            toDamage.GetDamaged(_damageAmount, Owner);
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
        Owner = null;
        _damageAmount = damageAmount;
    }
}
