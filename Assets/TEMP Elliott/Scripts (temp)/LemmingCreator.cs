using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingCreator : MonoBehaviour
{
    [SerializeField] private GameObject lemming;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] float offsetY = -1f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnObjectBelow();
        }
    }

    void SpawnObjectBelow()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);

        Instantiate(lemming, spawnPosition, Quaternion.identity);
    }
}
