using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] int numberOfHealth;
    [SerializeField] float chanceToSpawnHealth = 1;
    [SerializeField]
    [Range(0, 1)] float verticalOffset;

    private void Update()
    {
        Debug.DrawLine(new Vector3(-100, Camera.main.ViewportToWorldPoint(new Vector2(0, verticalOffset)).y), new Vector3(100, Camera.main.ViewportToWorldPoint(new Vector2(0, verticalOffset)).y));
    }

    public void SpawnHealth()
    {
        for (int i = 0; i < numberOfHealth; i++)
        {
            float randomNumber = Random.Range(1f, 100f);
            if (randomNumber < chanceToSpawnHealth)
            {
                Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value - verticalOffset));
                ObjectPooler.Instance.SpawnFromPool("Health", randomPositionOnScreen, Quaternion.identity);
            }
        }
    }

    public void ClearHealth()
    {
        GameObject[] healthOnScreen = GameObject.FindGameObjectsWithTag("Health");

        foreach(GameObject healthCapsule in healthOnScreen)
        {
            healthCapsule.gameObject.SetActive(false);
        }
    }
}
