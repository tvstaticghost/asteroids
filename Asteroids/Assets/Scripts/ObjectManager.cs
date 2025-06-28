using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject asteroidLarge;
    [SerializeField] GameObject asteroidLarge2;
    [SerializeField] GameObject asteroidSmall;
    [SerializeField] GameObject asteroidSmall2;
    [SerializeField] GameObject asteroidFragment;
    List<GameObject> asteroidOptions = new List<GameObject>();

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
            SpawnAsteroidFragment(impactPos);
        }
        else
        {
            SpawnSmallAsteroids(impactPos, moveDirection);
        }
    }

    private void SpawnSmallAsteroids(Vector3 impactPos, Vector3 moveDirection)
    {
        int asteroidChoiceIndex = Random.Range(2, 4);
        GameObject asteroidChoice = asteroidOptions[asteroidChoiceIndex];
        GameObject smallAsteroid = Instantiate(asteroidChoice, impactPos, Quaternion.identity);
        smallAsteroid.GetComponent<SmallAsteroid>().InitializeDirection(moveDirection, true);

        int secondChoiceIndex = Random.Range(2, 4);
        GameObject asteroidChoice2 = asteroidOptions[asteroidChoiceIndex];
        GameObject smallAsteroid2 = Instantiate(asteroidChoice, impactPos, Quaternion.identity);
        smallAsteroid2.GetComponent<SmallAsteroid>().InitializeDirection(moveDirection, false);
    }

    private void SpawnAsteroidFragment(Vector3 impactPos)
    {
        GameObject asteroidChoice = asteroidOptions[4];
        Instantiate(asteroidChoice, impactPos, Quaternion.identity);
    }
}
