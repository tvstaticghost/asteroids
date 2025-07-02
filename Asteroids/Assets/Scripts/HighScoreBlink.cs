using UnityEngine;

public class HighScoreBlink : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator.Play("blink");
    }
}
