using System.Collections;
using UnityEngine;

public class Crash : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Player player;
    void Start()
    {
        animator.Play("Crash");
        StartCoroutine(AnimateThenDestroy(1.3f)); //Animation Crash is 1.3s
    }

    private IEnumerator AnimateThenDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject); //Destroy after the animation is finished
    }
}
