using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //private IShootBullets shootBullets;
    protected BaseGun baseGun;

    protected virtual void Start()
    {
        baseGun = GetComponent<BaseGun>();
    }

    protected virtual void Update()
    {
        baseGun.Shoot();
    }
}
