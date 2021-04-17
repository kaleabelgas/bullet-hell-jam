using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    public void SetDirection(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
    }

}
