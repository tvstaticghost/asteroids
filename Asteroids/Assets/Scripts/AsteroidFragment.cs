using UnityEngine;

public class AsteroidFragment : AsteroidBase
{
    private bool clockwise;
    private int amountOfPoints = 100;

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
            AsteroidShot();
            uiController.IncreaseShotsHit();
            Destroy(collision.gameObject);
        }
    }

    private void AsteroidShot()
    {
        Destroy(gameObject);
        uiController.IncreaseScore(amountOfPoints);
    }
}
