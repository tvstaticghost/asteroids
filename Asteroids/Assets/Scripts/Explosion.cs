using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator.Play("Explosion");
        StartCoroutine(AnimateThenDestroy(0.17f));
    }

    //Explosion should delete itself after .17 seconds
    private IEnumerator AnimateThenDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
