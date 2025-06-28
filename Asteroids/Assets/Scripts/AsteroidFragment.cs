using UnityEngine;

public class AsteroidFragment : AsteroidBase
{
    private bool clockwise;

    private new void Start()
    {
        if (clockwise)
        {
            SetDirection("clockwise");
        }
        else
        {
            SetDirection("counterclockwise");
        }
    }
    public void InitializeDirection(Vector3 direction, bool clock)
    {
        base.moveDirection = direction;
        clockwise = clock;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AsteroidShot();
            Destroy(collision.gameObject);
        }
    }

    private void AsteroidShot()
    {
        Destroy(gameObject);
    }
}
