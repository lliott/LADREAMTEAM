using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingCreator : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] float offsetY = -1f;
    public bool isActivated = true;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true && isActivated)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnObjectBelow();
        }
    }

    void SpawnObjectBelow()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);

        LemmingPooler.Instance.SpawnFromPool("Lemming", spawnPosition, Quaternion.identity);
    }
}
