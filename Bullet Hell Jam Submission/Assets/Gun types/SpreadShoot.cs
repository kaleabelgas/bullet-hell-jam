using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShoot : BaseGun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private string bulletUsed;
    //[SerializeField] private Transform firePoint;
    [SerializeField] private int numPoints;
    [SerializeField] private float radius = 1;

    private float bulletTimer;

    public override void Shoot()
    {
        //Debug.Log("Called!");
        //bulletTimer += Time.deltaTime;

        if (Time.time >= bulletTimer)
        {
            for (int i = 0; i < numPoints; i++)
            {
                float angle = (i * 360 * Mathf.Deg2Rad) / numPoints;
                Vector2 firePoint = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));

                GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, (Vector2)transform.position + firePoint, transform.rotation);
                if (bullet == null)
                    return;
                BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
                bulletScript.Owner = gameObject;
                bulletScript.SetDirection(firePoint.normalized, bulletSpeed);
            }

            bulletTimer = Time.time + attackSpeed;
        }
    }
}
