using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] GameObject bullet;
    private Vector3 moveDirection;
    private Vector2 mousePos;
    private bool canShoot = true;
    private Collider2D playerCollider;
    public UIController uiController;
    public Crash crashScript;
    public ObjectManager objectManager;
    private Color originalColor;

    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        playerCollider = GetComponent<Collider2D>();
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            uiController.PlayerHit();
            PlayerCrash();
        }
    }

    private void PlayerCrash()
    {
        Vector3 animationLocation = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;
        gameObject.SetActive(false);
        objectManager.CreateCrash(animationLocation, rotation);
        //StartCoroutine(SpawnPlayer(1.3f));
    }

    public void PlayerReset()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        Color fadedColor = Color.white;
        fadedColor.a = 0.2f;
        gameObject.GetComponent<SpriteRenderer>().color = fadedColor;
        StartCoroutine(RestorePlayer(1f));
    }

    public IEnumerator RestorePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
    }

    void Update()
    {
       //Calculate Mouse position and rotate Player sprite to look at the mouse
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        //Change to detect mouse button being held to shoot every 0.2f seconds
        if (context.performed)
        {
            if (canShoot)
            {
                Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                canShoot = false;
                StartCoroutine(BulletTimer(0.2f));
            }
        }
    }

    public Vector2 GetMousePos()
    {
        return mousePos;
    }

    IEnumerator BulletTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
    }
}
