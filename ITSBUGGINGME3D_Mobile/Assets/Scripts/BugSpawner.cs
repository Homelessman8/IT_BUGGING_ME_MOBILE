using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public List<GameObject> bugs = new List<GameObject>();
    public List<GameObject> wave = new List<GameObject>();
    [SerializeField]
    float spawnRate = 5f;

    void Start()
    {
        StartCoroutine(spawner(spawnRate));   
    }

    IEnumerator spawner(float spawnRate)
    {
        while(true)
        {
            Instantiate(bugs[Random.Range(0, 4)], transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}