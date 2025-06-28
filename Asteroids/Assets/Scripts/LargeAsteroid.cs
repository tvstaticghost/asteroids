using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LargeAsteroid : AsteroidBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Shot");
            AsteroidHit();
            Destroy(collision.gameObject);
        }
    }

    private void AsteroidHit()
    {
        objectManager.LargeAsteroidExplosion(gameObject.transform.position, moveDirection);
        Destroy(gameObject);
    }
}
