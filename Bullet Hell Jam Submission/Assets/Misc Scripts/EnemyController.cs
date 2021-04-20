using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITakeDamage
{
    public int EnemyHealth { get; private set; }
    [SerializeField] float speed;
    [SerializeField] private GameObject target;
    [SerializeField] private int health;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        EnemyHealth = health;
    }

    private void Update()
    {

        Vector2 lookDir = target.transform.position - transform.position;
        lookDir.Normalize();
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270;

        transform.Translate(Vector2.down * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        //transform.right = target.position - transform.position;
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(gameObject))
            return;
        EnemyHealth -= amount;
        //Debug.Log("Enemy Health: " + EnemyHealth);
        if (EnemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
