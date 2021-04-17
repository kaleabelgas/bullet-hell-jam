using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private IShootBullets shootBullets;

    private void Start()
    {
        shootBullets = GetComponent<IShootBullets>();
    }

    private void Update()
    {
        shootBullets.Shoot();
    }
}
