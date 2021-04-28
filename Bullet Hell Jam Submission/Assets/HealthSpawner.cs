using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] int numberOfHealth;
    [SerializeField] float chanceToSpawnHealth = 1;
    [SerializeField]
    [Range(0, 1)] float verticalOffset;

    [SerializeField] float healthPercentage;

    private PlayerController playerController;

    private Camera cam;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        cam = Camera.main;
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-100, cam.ViewportToWorldPoint(new Vector2(0, verticalOffset)).y), new Vector3(100, cam.ViewportToWorldPoint(new Vector2(0, verticalOffset)).y));
    }

    public void SpawnHealth()
    {
        if(playerController.Health / playerController.playerHealth <= healthPercentage)
        {
            for (int i = 0; i < numberOfHealth; i++)
            {
                float randomNumber = Random.Range(1f, 100f);
                if (randomNumber < chanceToSpawnHealth)
                {
                    Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint
                        (new Vector2(Random.value, Mathf.Clamp(Random.value, verticalOffset, 1 - verticalOffset)));
                    ObjectPooler.Instance.SpawnFromPool("Health", randomPositionOnScreen, Quaternion.identity);
                }
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
