using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : DefaultBulletScript
{
    private GameManager gm;

    private const float overFive = .25f;

    protected override void OnEnable()
    {
        base.OnEnable();
        gm = FindObjectOfType<GameManager>();
        _damageAmount += Mathf.FloorToInt(gm.CurrentWave * overFive);
        //Debug.Log("Damage: " + _damageAmount, this);
    }
}
