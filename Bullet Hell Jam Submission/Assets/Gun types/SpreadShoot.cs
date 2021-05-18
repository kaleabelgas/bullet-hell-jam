using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShoot : BaseGun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;
    //[SerializeField] private Transform firePoint;
    [SerializeField] private int numSides;
    [SerializeField] private float radius = 1;

    private Vector3 point;
    private Vector3 direction;
    [SerializeField] private float arc = 360;
    private float bulletTimer;

    public override void Shoot()
    {
        if (Time.time >= bulletTimer)
        {
            for (int i = 0; i < numSides; i++)
            {
                var angle = Mathf.Deg2Rad * (2 * arc * i - arc * numSides + arc + 180 * numSides) / (2 * numSides);
                point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                point = Quaternion.LookRotation(transform.forward, transform.up) * point;
                direction = point.normalized;
                point += transform.position;

                GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, point, Quaternion.identity);
                BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
                bulletScript.SetDirection(direction, bulletSpeed, gameObject);
            }
            bulletTimer = Time.time + attackSpeed;
        }
    }
}
