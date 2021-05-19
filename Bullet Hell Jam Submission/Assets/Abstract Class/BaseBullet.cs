using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected int damageAmount;

    protected int _damageAmount;
    protected GameObject Owner;

    protected ObjectPooler objectPooler;

    private bool hasReset = true;

    protected virtual void OnEnable()
    {
        _damageAmount = damageAmount;
        objectPooler = ObjectPooler.Instance;
    }

    public virtual void SetDirection(Vector2 direction, float speed, GameObject owner)
    {
        Owner = owner;
        if (!hasReset) { Debug.LogError("Bullet Reused! " + gameObject.name); }
        hasReset = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Owner == null) { throw new System.Exception("ERROR: HIT WITHOUT OWNER"); }

        ITakeDamage toDamage = other.GetComponent<ITakeDamage>();
        if (toDamage == null) { return; }

        if (!Owner.CompareTag(other.gameObject.tag))
        {
            objectPooler.SpawnFromPool("hit effect", transform.position, transform.rotation);
            toDamage.GetDamaged(_damageAmount); ;
        }



        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy) { objectPooler.SpawnFromPool("hit effect", transform.position, transform.rotation); }
        
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        //Owner = null;
        _damageAmount = damageAmount;
        hasReset = true;
    }
}
