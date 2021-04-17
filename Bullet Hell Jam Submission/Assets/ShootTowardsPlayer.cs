using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowardsPlayer : MonoBehaviour, IShootBullets
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;

    private float bulletTimer;

    private GameObject player;
    private Vector2 playerPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        //Debug.Log("Called!");
        playerPosition = player.transform.position;
        //bulletTimer += Time.deltaTime;

        if (Time.time >= bulletTimer)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, transform.position, transform.rotation);
            if (bullet == null)
                return;
            bullet.GetComponent<BulletScript>().SetDirection((playerPosition - (Vector2)transform.position).normalized, bulletSpeed);
            Debug.Log((playerPosition - (Vector2)transform.position).normalized);
            bulletTimer = Time.time + attackSpeed;
            Debug.Log("GO");
        }


    }
}
