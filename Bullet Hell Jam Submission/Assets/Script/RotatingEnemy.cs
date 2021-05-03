using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour, ITakeDamage
{
    public int EnemyHealth { get; private set; }
    [SerializeField] float speed;
    [SerializeField] private int health;
    [SerializeField] private int enemyScore;

    private Vector2 screenBounds;
    [SerializeField] float offset = 10;
    [SerializeField] float rotationUpdateSpeed;


    public event Action iHaveDied;
    private UIMainGame UIMainGame;
    private void OnEnable()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, Camera.main.transform.position.z));
        EnemyHealth = health;
        StartCoroutine(SpiralEnemy());

        UIMainGame = FindObjectOfType<UIMainGame>();
        iHaveDied += UIMainGame.UpdateScore;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
        //offset *= (Mathf.Sin(Time.time) + 1) * 0.5f;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = transform.position.x;
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
        if(transform.position.y < viewPos.y)
            transform.position = viewPos;
    }



    private IEnumerator SpiralEnemy()
    {
        float angle = 0;
        float _offset = offset;
        while (true)
        {
            //Quaternion initialRot = transform.rotation;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
            angle += _offset;
            yield return new WaitForSeconds(rotationUpdateSpeed);
        }
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.CompareTag(gameObject.tag))
            return;
        EnemyHealth -= amount;
        CameraShake.Trauma = 0.4f;
        if (EnemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        AudioManager.instance.Play("enemy ded");
        ObjectPooler.Instance.SpawnFromPool("death effect", transform.position, transform.rotation);
        CameraShake.Trauma = 0.75f;
        EnemyCounter.AddToScore(enemyScore);
        iHaveDied?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        iHaveDied -= UIMainGame.UpdateScore;
    }
}
