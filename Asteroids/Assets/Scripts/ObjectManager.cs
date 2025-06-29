using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Start()
    {
        asteroidOptions.Add(asteroidLarge);
        asteroidOptions.Add(asteroidLarge2);
        asteroidOptions.Add(asteroidSmall);
        asteroidOptions.Add(asteroidSmall2);
        asteroidOptions.Add(asteroidFragment);
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
        player.SetActive(true);
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
}
