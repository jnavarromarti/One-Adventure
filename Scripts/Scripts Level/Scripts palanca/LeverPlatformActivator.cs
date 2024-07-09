using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPlatformActivator : MonoBehaviour
{
   [SerializeField] public PlatformMovement platform;
   [SerializeField] public GameObject lever;
   Animator animator;
void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("ActivateT", true);
        yield return new WaitForSeconds(0.5f);
        platform.GetComponent<PlatformMovement>().enabled = true;
    }
}
