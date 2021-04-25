using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour, ITakeDamage
{
    public int EnemyHealth { get; private set; }
    [SerializeField] float speed;
    [SerializeField] private int health;

    private Vector2 screenBounds;

    private void OnEnable()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, Camera.main.transform.position.z));
        EnemyHealth = health;
        StartCoroutine(SpiralEnemy());
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
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
        float offset = 10;
        while (true)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
            yield return new WaitForSeconds(0.05f);
            angle += offset;
        }
    }

    public void GetDamaged(int amount, GameObject owner)
    {
        if (owner.Equals(gameObject))
            return;
        EnemyHealth -= amount;
        CameraShake.Trauma = 0.2f;
        if (EnemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        AudioManager.instance.Play("enemy ded");
        ObjectPooler.Instance.SpawnFromPool("death effect", transform.position, transform.rotation);
        CameraShake.Trauma = 0.65f;
        gameObject.SetActive(false);
    }
}
