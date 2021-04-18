using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableGunScript : GunScript
{

    protected override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            base.Update();
        }
    }
}
