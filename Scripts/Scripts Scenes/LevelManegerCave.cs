using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManegerCave : MonoBehaviour
{
    //Level Manager del cave to wood 2, si este se activa volvemos a la superficie despues de darle a la palanca
    HeroController heroController;
    public Transform hero;
    public bool levelComplete;
    public float timeToWait;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (levelComplete)
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelComplete = true;
            heroController.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelComplete = false;
        }
    }
    IEnumerator LoadNextLevel()
    {
        animator.SetBool("Fadeout", true);
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("Wood2");
        yield return new WaitForSeconds(timeToWait);
        animator.SetBool("Fadeout", false);
    }
}
