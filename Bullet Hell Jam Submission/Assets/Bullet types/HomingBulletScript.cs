using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletScript : BaseBullet
{
    private Vector2 direction;
    private Vector2 playerDirection;
    private float speed;

    [SerializeField] private float homingSpeed;
    [SerializeField] private float timeBeforeFollow = 2;

    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void SetDirection(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    private void Update()
    {
        playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        timeBeforeFollow -= Time.deltaTime;
        if (timeBeforeFollow > 0)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            speed = homingSpeed;
            transform.Translate(playerDirection * speed * Time.deltaTime);
        }
    }

}
