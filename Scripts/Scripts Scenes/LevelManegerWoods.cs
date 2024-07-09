using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManegerWoods : MonoBehaviour
{
    //Level maneger que ejecuta la transicion fadeout entre la cueva y la segunda parte del nivel woods
   [SerializeField] public LevelManegerCave levelManeger;
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
        levelManeger.GetComponent<LevelManegerCave>().enabled = true;
    }

}
