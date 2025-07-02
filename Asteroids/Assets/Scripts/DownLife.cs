using UnityEngine;

public class DownLife : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator.Play("DownLife");
        Destroy(gameObject, 1.2f);
    }
}
