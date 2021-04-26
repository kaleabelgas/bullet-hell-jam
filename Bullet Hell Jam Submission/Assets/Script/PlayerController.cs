using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float focusAmount;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int playerHealth;
    private UIMainGame uIMainGame;

    private float _focusAmount = 1;
    private float _playerSpeed;

    public event Action OnPlayerDeath;
    private Vector2 direction;
    private Rigidbody2D playerRB2D;

    public int Health { get; private set; } = 100;

    private void Awake()
    {
        uIMainGame = FindObjectOfType<UIMainGame>();
        OnPlayerDeath += gameManager.GameOver;
        playerRB2D = GetComponent<Rigidbody2D>();
        Health = playerHealth;
        uIMainGame.SetHealthBarMax(playerHealth);
    }



    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _focusAmount = focusAmount;
        }
        else
            _focusAmount = 1;

        _playerSpeed = playerSpeed * _focusAmount;
    }

    private void FixedUpdate()
    {
        playerRB2D.MovePosition(playerRB2D.position + direction * _playerSpeed * Time.deltaTime);
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(this.gameObject))
            return;
        Health -= amount;
        if(amount > 0) CameraShake.Trauma = 0.7f;
        if (Health > playerHealth) 
            Health = playerHealth;
        uIMainGame.SetHealthBar(Health);
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        ObjectPooler.Instance.SpawnFromPool("death effect", transform.position, transform.rotation);
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
