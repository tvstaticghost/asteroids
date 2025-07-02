using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject asteroidLarge;
    [SerializeField] GameObject asteroidLarge2;
    [SerializeField] GameObject asteroidSmall;
    [SerializeField] GameObject asteroidSmall2;
    [SerializeField] GameObject asteroidFragment;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject crash;
    [SerializeField] GameObject player;
    List<GameObject> asteroidOptions = new List<GameObject>();
    public int playerLives = 3;

    [SerializeField] private float screenTop = 6f;
    [SerializeField] private float screenBottom = -6f;
    [SerializeField] private float screenLeft = -10.15f;
    [SerializeField] private float screenRight = 10.15f;

    public enum SpawnEdge { Top, Bottom, Left, Right }

    private List<SpawnEdge> spawnEdges = new List<SpawnEdge>
    {
        SpawnEdge.Top,
        SpawnEdge.Bottom,
        SpawnEdge.Left,
        SpawnEdge.Right
    };

    private Coroutine spawnCoroutine; //Creating a variable with the Coroutine so I can stop it later

    private void Start()
    {
        asteroidOptions.Add(asteroidLarge);
        asteroidOptions.Add(asteroidLarge2);
        asteroidOptions.Add(asteroidSmall);
        asteroidOptions.Add(asteroidSmall2);
        asteroidOptions.Add(asteroidFragment);

        spawnCoroutine = StartCoroutine(SpawnAsteroidsRoutine());
    }

    public void LargeAsteroidExplosion(Vector3 impactPos, Vector3 moveDirection)
    {
        int fragmentSpawnChance = Random.Range(1, 11);
        if (fragmentSpawnChance == 10)
        {
            SpawnAsteroidFragment(impactPos, moveDirection);
        }
        else
        {
            SpawnSmallAsteroids(impactPos, moveDirection);
        }
    }

    public void CreateExplosion(Vector3 impactPos)
    {
        Instantiate(explosion, impactPos, Quaternion.identity);
    }

    public void CreateCrash(Vector3 crashPos, Quaternion crashRotation)
    {
        playerLives--;
        Instantiate(crash, crashPos, crashRotation);
        if (playerLives > 0)
        {
            StartCoroutine(SpawnPlayer(1.5f));
        }
    }

    private IEnumerator SpawnPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Player>().PlayerReset();
    }

    private void SpawnSmallAsteroids(Vector3 impactPos, Vector3 moveDirection)
    {
        int asteroidChoiceIndex = Random.Range(2, 4);
        GameObject asteroidChoice = asteroidOptions[asteroidChoiceIndex];
        GameObject smallAsteroid = Instantiate(asteroidChoice, impactPos, Quaternion.identity);
        smallAsteroid.GetComponent<SmallAsteroid>().InitializeDirection(moveDirection, true);

        int secondChoiceIndex = Random.Range(2, 4);
        GameObject asteroidChoice2 = asteroidOptions[secondChoiceIndex];
        GameObject smallAsteroid2 = Instantiate(asteroidChoice2, impactPos, Quaternion.identity);
        smallAsteroid2.GetComponent<SmallAsteroid>().InitializeDirection(moveDirection, false);
    }

    private void SpawnAsteroidFragment(Vector3 impactPos, Vector3 moveDirection)
    {
        GameObject asteroidChoice = asteroidOptions[4];
        GameObject asteroidFrag = Instantiate(asteroidChoice, impactPos, Quaternion.identity);
        asteroidFrag.GetComponent<AsteroidFragment>().InitializeDirection(moveDirection, true);

        int secondChoiceIndex = Random.Range(2, 4);
        GameObject asteroidChoice2 = asteroidOptions[secondChoiceIndex];
        GameObject smallAsteroid2 = Instantiate(asteroidChoice2, impactPos, Quaternion.identity);
        smallAsteroid2.GetComponent<SmallAsteroid>().InitializeDirection(moveDirection, false);
    }

    private Vector3 GenerateAsteroidCoordinates(SpawnEdge edge)
    {
        return edge switch
        {
            SpawnEdge.Top => new Vector3(Random.Range(screenLeft, screenRight), screenTop, 0),
            SpawnEdge.Bottom => new Vector3(Random.Range(screenLeft, screenRight), screenBottom, 0),
            SpawnEdge.Left => new Vector3(screenLeft, Random.Range(screenBottom, screenTop), 0),
            SpawnEdge.Right => new Vector3(screenRight, Random.Range(screenBottom, screenTop), 0),
            _ => throw new ArgumentOutOfRangeException(nameof(edge), edge, null),
        };
    }

    private void SpawnLargeAsteroids()
    {
        SpawnEdge randomEdge = spawnEdges[Random.Range(0, spawnEdges.Count)];
        Vector3 spawnPosition = GenerateAsteroidCoordinates(randomEdge);
        Instantiate(asteroidLarge, spawnPosition, Quaternion.identity);
    }

    //Tweek the amount of time for asteroids over time
    private IEnumerator SpawnAsteroidsRoutine()
    {
        while (true)
        {
            SpawnLargeAsteroids();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StopSpawningAsteroids()
    {
        Debug.Log("Stopping Asteroids");
        StopCoroutine(spawnCoroutine);
    }

    public void RemoveAllAsteroids()
    {
        GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in allAsteroids)
        {
            Destroy(asteroid);
        }
    }

    public void RestartGame()
    {
        playerLives = 3;
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnAsteroidsRoutine());
        RemoveAllAsteroids();
    }
}
