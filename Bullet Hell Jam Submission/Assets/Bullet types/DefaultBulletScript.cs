using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBulletScript : BaseBullet
{
    private Vector2 direction;
    private float speed;
    public override void SetDirection(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle, Space.World);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

}
