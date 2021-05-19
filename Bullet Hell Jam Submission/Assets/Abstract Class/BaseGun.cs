using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    protected ObjectPooler objectPooler;
    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }
    public virtual void Shoot()
    {

    }
}
