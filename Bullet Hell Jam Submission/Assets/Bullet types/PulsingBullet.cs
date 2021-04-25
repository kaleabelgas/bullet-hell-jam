using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingBullet : BaseBullet
{
    private Vector2 direction;
    private float speed;
    public override void SetDirection(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime * 0.5f * (Mathf.Sin(Time.time * 10) + 1.1f));
    }

}