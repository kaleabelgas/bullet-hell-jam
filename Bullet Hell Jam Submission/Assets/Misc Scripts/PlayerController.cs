using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int playerSpeed;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int playerHealth;
    private ControllerUI controllerUI;

    public event Action OnPlayerDeath;
    private Vector2 direction;
    private Rigidbody2D playerRB2D;

    public int Health { get; private set; } = 100;

    private void Awake()
    {
        controllerUI = FindObjectOfType<ControllerUI>();
        OnPlayerDeath += gameManager.GameOver;
        playerRB2D = GetComponent<Rigidbody2D>();
        Health = playerHealth;

        controllerUI.SetHealthBarMax(playerHealth);
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

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(this.gameObject))
            return;
        Health -= amount;
        CameraShake.Trauma = 0.7f;
        //Debug.Log("Health: " + Health);
        controllerUI.SetHealthBar(Health);
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
