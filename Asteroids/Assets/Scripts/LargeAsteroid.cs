using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LargeAsteroid : AsteroidBase
{
    private int amountOfPoints = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AsteroidHit();
            uiController.IncreaseShotsHit();
            Destroy(collision.gameObject);
        }
    }

    private void AsteroidHit()
    {
        objectManager.LargeAsteroidExplosion(gameObject.transform.position, moveDirection);
        Destroy(gameObject);
        objectManager.CreateExplosion(transform.position);
        uiController.IncreaseScore(amountOfPoints);
    }
}
