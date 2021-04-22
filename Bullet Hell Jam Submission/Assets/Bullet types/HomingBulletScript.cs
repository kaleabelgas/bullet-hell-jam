using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletScript : BaseBullet
{
    private Vector2 lastDirection;
    private Vector2 targetDirection;
    private float speed;
    private float timeFollowing;

    [SerializeField] private Transform bulletTarget;

    [SerializeField] private float defaultTimeFollowing = 2;
    
    private void OnEnable()
    {
        timeFollowing = defaultTimeFollowing;
    }

    public override void SetDirection(Vector2 direction, float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        targetDirection = bulletTarget.transform.position - transform.position;
        targetDirection.Normalize();

        timeFollowing -= Time.deltaTime;
        if (timeFollowing > 0)
        {
            transform.Translate(targetDirection * speed * Time.deltaTime);
            lastDirection = targetDirection;
        }
        else
        {
            transform.Translate(lastDirection * speed * Time.deltaTime);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        bulletTarget = null;
        lastDirection = Vector2.zero;
        timeFollowing = 0;
    }
}
