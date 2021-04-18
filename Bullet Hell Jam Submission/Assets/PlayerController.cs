using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int playerSpeed;

    private Vector2 direction;
    //private Vector2 mousePosition;
    private Rigidbody2D playerRB2D;

    public int Health { get; private set; }

    private void Start()
    {
        playerRB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

    }

    private void FixedUpdate()
    {
        playerRB2D.MovePosition(playerRB2D.position + direction * playerSpeed * Time.deltaTime);
    }

    public void GetDamaged(int amount)
    {
        Health -= amount;
        Debug.Log(Health);
    }
}
