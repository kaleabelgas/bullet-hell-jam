using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float focusAmount;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private GameManager gameManager;
    public int playerHealth;
    private UIMainGame uIMainGame;

    private float _focusAmount = 1;
    private float _playerSpeed;
    private float _dashCooldown;

    public event Action OnPlayerDeath;
    private Vector2 direction;
    private Vector2 lastDirection;
    private Rigidbody2D playerRB2D;

    private Camera cam;
    private Vector2 mousePos;
    private Vector2 screenBounds;

    bool doDash;
    bool isInvincible;

    float angle;

    public int Health { get; private set; } = 100;

    private void Awake()
    {
        uIMainGame = FindObjectOfType<UIMainGame>();
        OnPlayerDeath += gameManager.GameOver;
        playerRB2D = GetComponent<Rigidbody2D>();
        Health = playerHealth;
        uIMainGame.SetHealthBarMax(playerHealth);

        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }



    private void Update()
    {


        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _focusAmount = focusAmount;
        }
        else
            _focusAmount = 1;

        _playerSpeed = playerSpeed * _focusAmount;

        if (Input.GetKeyDown(KeyCode.Space) && _dashCooldown <= 0)
        {
            doDash = true;
        }

        


        if (Input.GetKey(KeyCode.F1))
        {
            KillAllCheat();
        }



    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = transform.position.x;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x, screenBounds.x);

        if (transform.position.x < viewPos.x)
            transform.position = viewPos;
        if (transform.position.x > -viewPos.x)
            transform.position = viewPos;



    }

    private void FixedUpdate()
    {


        _dashCooldown -= Time.deltaTime;

        if (doDash)
        {
            StartCoroutine(Dash(dashTime));
        }

        if (_dashCooldown < dashCooldown - dashTime)
        {
            Move();
        }

        Vector2 lookDir = mousePos - (Vector2)transform.position;
        lookDir.Normalize();
        //Debug.Log(lookDir);
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

    }


    private void Move()
    {
        playerRB2D.MovePosition(playerRB2D.position + direction * _playerSpeed * Time.deltaTime);

        if (Mathf.Abs(direction.x) > 0.5f || Mathf.Abs(direction.y) > 0.5f)
        {
            lastDirection = direction;
        }
    }

    private IEnumerator Dash(float _dashTime)
    {
        float _dashSpeed = dashDistance / dashTime;
        _dashCooldown = dashCooldown;
        while (_dashTime > 0)
        {
            _dashTime -= Time.deltaTime;
            Debug.Log(_dashCooldown);
            // add a force based on last direction looked at, with dashForce amount of force, multiplied to Time.deltaTime so that it's constant.
            //playerRB2D.AddForce(lastDirection * dashForce * Time.deltaTime, ForceMode2D.Impulse);
            isInvincible = true;
            playerRB2D.MovePosition(playerRB2D.position + lastDirection * _dashSpeed * Time.deltaTime);
            yield return null;
        }

        // this is outside the while loop so that we know to stop the function when the time specified in dashTime is done
        doDash = false;
        isInvincible = false;
        //Object.Destroy(dashParticle);
        yield return null;
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (isInvincible)
            return;
        if (owner.Equals(this.gameObject))
            return;
        Health = Mathf.Min(playerHealth, Health - amount);
        if (amount > 0)
        {
            CameraShake.Trauma = 0.7f;
            AudioManager.instance.Play("player hit");
            ObjectPooler.Instance.SpawnFromPool("player hit", transform.position, transform.rotation);
        }
        //if (Health > playerHealth) 
        //    Health = playerHealth;
        uIMainGame.SetHealthBar(Health);
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        //ObjectPooler.Instance.SpawnFromPool("death effect", transform.position, transform.rotation);
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void KillAllCheat()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] _bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject _enemy in _enemies)
        {
            _enemy.SetActive(false);
        }

        foreach (GameObject _bullet in _bullets)
        {
            _bullet.SetActive(false);
        }
    }
}
