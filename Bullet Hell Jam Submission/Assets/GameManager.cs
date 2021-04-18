using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private int chanceToSpawnEnemy = 1;

    private float timePerLevel = 10;
    private float timeRemaining;

    public int Level { get; private set; } = 1;

    
    void Start()
    {
        timeRemaining = timePerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        // timer

        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = timePerLevel;
            Level++;
            Debug.Log("Current Level: " + Level);
            //do trigget stuff
        }
    }

    List<GameObject> ChooseEnemies(List<GameObject> enemiesToChooseFrom)
    {
        List<GameObject> chosenEnemies = new List<GameObject>();
        
        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            chosenEnemies.Add(enemiesToChooseFrom[0]);
            // RNG
            int randomNumber = Random.Range(1, 101);
            if (randomNumber < chanceToSpawnEnemy)
                chosenEnemies.Add(enemiesToChooseFrom[i]);
        }
        return chosenEnemies;
    }


}
