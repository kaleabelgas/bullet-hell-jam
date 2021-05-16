using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : DefaultBulletScript
{
    private GameManager gm;

    private const float ten = 10;

    protected override void OnEnable()
    {
        base.OnEnable();
        gm = FindObjectOfType<GameManager>();
        _damageAmount += Mathf.FloorToInt(gm.CurrentWave / ten);
        Debug.Log(_damageAmount, this);
    }
}
