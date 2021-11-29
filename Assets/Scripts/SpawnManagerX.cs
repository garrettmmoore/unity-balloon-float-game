using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private const float SpawnDelay = 2;
    private const float SpawnInterval = 1.5f;

    private PlayerControllerX _playerControllerScript;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), SpawnDelay, SpawnInterval);
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Spawn obstacles
    private void SpawnObjects ()
    {
        // Set random spawn location and random object index
        var spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        var index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!_playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
