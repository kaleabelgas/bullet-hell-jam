using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAdder : MonoBehaviour
{
    [SerializeField] private int damageAmount = -10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        ITakeDamage toDamage = other.GetComponent<ITakeDamage>();

        if (toDamage == null || !other.gameObject.CompareTag("Player")) { return; }
        //AudioManager.instance.Play("hit");
        toDamage.GetDamaged(damageAmount);
        AudioManager.instance.Play("powerup");
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ObjectPooler.Instance.SpawnFromPool("collect health", transform.position, Quaternion.identity);
    }
}
