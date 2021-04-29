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


    private Camera maincam;

    private void Start()
    {
        maincam = Camera.main;
    }
    public override void Shoot()
    {
        mousePosition = maincam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePosition - (Vector2)firePoint.position;
        lookDir.Normalize();
        //Debug.Log(lookDir);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, angle);

        if (Time.time >= bulletTimer)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletUsed, firePoint.position, Quaternion.identity);
            if (bullet == null)
                return;
            BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
            bulletScript.Owner = gameObject;
            bulletScript.SetDirection(lookDir, bulletSpeed);
            bulletTimer = Time.time + attackSpeed;
            AudioManager.instance.Play("hit");
            //CameraShake.Trauma = 0.35f;
        }
    }
}
