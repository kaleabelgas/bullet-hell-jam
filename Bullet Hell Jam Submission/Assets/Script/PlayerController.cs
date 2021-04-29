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
    public int playerHealth;
    private UIMainGame uIMainGame;

    private float _focusAmount = 1;
    private float _playerSpeed;

    public event Action OnPlayerDeath;
    private Vector2 direction;
    private Rigidbody2D playerRB2D;

    private Camera cam;
    private Vector2 mousePos;

    public int Health { get; private set; } = 100;

    private void Awake()
    {
        uIMainGame = FindObjectOfType<UIMainGame>();
        OnPlayerDeath += gameManager.GameOver;
        playerRB2D = GetComponent<Rigidbody2D>();
        Health = playerHealth;
        uIMainGame.SetHealthBarMax(playerHealth);

        cam = Camera.main;
    }



    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - (Vector2)transform.position;
        lookDir.Normalize();
        //Debug.Log(lookDir);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

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


        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.M))
        {
            KillAllCheat();
        }

    }

    private void FixedUpdate()
    {
        playerRB2D.MovePosition(playerRB2D.position + direction * _playerSpeed * Time.deltaTime);
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(this.gameObject))
            return;
        Health = Mathf.Min(playerHealth, Health - amount);
        if (amount > 0)
        {
            CameraShake.Trauma = 0.7f;
            AudioManager.instance.Play("player hit");
        }
        //if (Health > playerHealth) 
        //    Health = playerHealth;
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

    private void KillAllCheat()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] _bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach(GameObject _enemy in _enemies)
        {
            _enemy.SetActive(false);
        }

        foreach(GameObject _bullet in _bullets)
        {
            _bullet.SetActive(false);
        }
    }
}
