using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowardsPlayer : BaseGun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;
    [SerializeField] private Transform firePoint;

    private float bulletTimer;

    private GameObject player;
    private Vector2 playerPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Shoot()
    {
        //Debug.Log("Called!");
        playerPosition = player.transform.position;
        Vector2 targetPosition = playerPosition - (Vector2)transform.position;

        //bulletTimer += Time.deltaTime;

        if (Time.time >= bulletTimer)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletUsed, firePoint.position, Quaternion.identity);
            if (bullet == null)
                return;
            BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
            bulletScript.SetDirection(targetPosition.normalized, bulletSpeed, gameObject);
            //Debug.Log(gameObject.tag, bullet);
            bulletTimer = Time.time + attackSpeed;
        }
    }
}
