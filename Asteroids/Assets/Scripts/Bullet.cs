using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Player playerScript;
    [SerializeField] float bulletSpeed = 15f;

    void Start()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Camera mainCamera = Camera.main;

        transform.position = GameObject.FindGameObjectWithTag("BulletSpawn").transform.position;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(playerScript.GetMousePos().x, playerScript.GetMousePos().y, mainCamera.nearClipPlane));
        mouseWorldPos.z = 0f;

        direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rigidBody.linearVelocity = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.linearVelocity = direction.normalized * bulletSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
