using UnityEngine;

public class SmallAsteroid : AsteroidBase
{
    private bool clockwise;
    private int amountOfPoints = 70;
    private new void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();

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
            Destroy(collision.gameObject);
            uiController.IncreaseShotsHit();
            AsteroidShot();
        }
    }

    private void AsteroidShot()
    {
        Destroy(gameObject);
        uiController.IncreaseScore(amountOfPoints);
    } 
}
