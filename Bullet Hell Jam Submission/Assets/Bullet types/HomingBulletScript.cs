using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletScript : BaseBullet
{
    private Vector2 lastDirection;
    private Vector2 playerDirection;
    private float speed;
    private float timeFollowing;

    [SerializeField] private float defaultTimeFollowing = 2;

    private GameObject player;

    private void OnEnable()
    {
        timeFollowing = defaultTimeFollowing;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void SetDirection(Vector2 direction, float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        timeFollowing -= Time.deltaTime;
        if (timeFollowing > 0)
        {
            transform.Translate(playerDirection * speed * Time.deltaTime);
            lastDirection = playerDirection;
        }
        else
        {
            transform.Translate(lastDirection * speed * Time.deltaTime);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        player = null;
        lastDirection = Vector2.zero;
        timeFollowing = 0;
    }
}
