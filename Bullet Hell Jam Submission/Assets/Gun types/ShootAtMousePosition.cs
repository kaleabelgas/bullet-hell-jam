using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtMousePosition : BaseGun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;

    private float bulletTimer;

    private Vector2 mousePosition;

    public override void Shoot()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Time.time >= bulletTimer)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, transform.position, transform.rotation);
            if (bullet == null)
                return;
            bullet.GetComponent<BaseBullet>().SetDirection(mousePosition.normalized, bulletSpeed);
            bulletTimer = Time.time + attackSpeed;
        }
    }
}
