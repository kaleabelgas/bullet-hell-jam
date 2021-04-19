using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtMousePosition : BaseGun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;
    [SerializeField] private Transform firePoint;

    private float bulletTimer;

    private Vector2 mousePosition;

    public override void Shoot()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Time.time >= bulletTimer)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, firePoint.position, transform.rotation);
            if (bullet == null)
                return;
            BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
            bulletScript.Owner = gameObject;
            bulletScript.SetDirection((mousePosition -(Vector2)firePoint.position).normalized, bulletSpeed);
            bulletTimer = Time.time + attackSpeed;
        }
    }
}
