using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;

    public int currentWaveIndex = 0;
    public Wave[] waves;
    
    private bool readyToCountDown;

    private void Start()
    {
        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    private void Update()
    {
        // Check if waves are configured
        if (waves.Length == 0)
        {
            Debug.Log("No waves configured!");
            return;
        }

        if (readyToCountDown)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            readyToCountDown = false;
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        // Handle wave completion
        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            Debug.Log("WAVE COMPLETED! NEXT WAVE INCOMING!");
            currentWaveIndex++;

            // Reset to first wave for infinite mode
            if (currentWaveIndex >= waves.Length)
            {
                currentWaveIndex = 0; // Reset to first wave
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
                waves[currentWaveIndex].enemiesLeft -= 1;
                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}