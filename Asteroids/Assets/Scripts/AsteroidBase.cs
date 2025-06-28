using UnityEngine;

public class AsteroidBase : MonoBehaviour
{
    protected Vector3 moveDirection;
    protected Vector3 playerPos;
    protected ObjectManager objectManager;
    protected float movementSpeed = 2f;

    protected void Start()
    {
        objectManager = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        moveDirection = (playerPos - transform.position).normalized;
    }

    void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * moveDirection;
    }

    public void SetDirection(string direction)
    {
        float angleDeg;
        if (direction == "clockwise")
        {
            angleDeg = -30f;
        }
        else
        {
            angleDeg = 30f;
        }

        float angleRad = angleDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);

        Vector2 rotatedDirection = new Vector2(
            moveDirection.x * cos - moveDirection.y * sin,
            moveDirection.x * sin + moveDirection.y * cos
        );

        moveDirection = (Vector3)(rotatedDirection);
    }
}
