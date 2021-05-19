using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITakeDamage
{
    public int EnemyHealth { get; private set; }
    [SerializeField] float speed;
    [SerializeField] private GameObject target;
    [SerializeField] private int health;
    [SerializeField] private int enemyScore;

    public event Action iHaveDied;
    private UIMainGame UIMainGame;

    private Vector2 lookDir = Vector2.up;
    private float angle = 90;

    private ObjectPooler objectPooler;
    private AudioManager audioManager;

    private void OnEnable()
    {
        audioManager = AudioManager.instance;
        objectPooler = ObjectPooler.Instance;

        target = GameObject.FindGameObjectWithTag("Player");
        EnemyHealth = health;
        UIMainGame = FindObjectOfType<UIMainGame>();
        iHaveDied += UIMainGame.UpdateScore;
    }

    private void Update()
    {

        if(target != null) 
        {
            lookDir = target.transform.position - transform.position;
            lookDir.Normalize();
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        }
        
        

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        //transform.right = target.position - transform.position;
    }

    public void GetDamaged(int amount)
    {
        EnemyHealth -= amount;
        CameraShake.Trauma = 0.4f;

        //Debug.Log("Hit by " + owner.name);
        if (EnemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        audioManager.Play("enemy ded");
        objectPooler.SpawnFromPool("death effect", transform.position, transform.rotation);

        CameraShake.Trauma = 0.7f;
        EnemyCounter.AddToScore(enemyScore);
        iHaveDied?.Invoke();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        iHaveDied -= UIMainGame.UpdateScore;
    }
}
