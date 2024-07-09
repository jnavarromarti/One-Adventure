using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManegerWood : MonoBehaviour
{
    //Otro level manager que detecta si el heroe ha colisionado con el y si es cierto desactiva sus contgroles y ejecuta la transicion entre niveles, en este caso a cave.
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
        SceneManager.LoadScene("Cave");
        yield return new WaitForSeconds(timeToWait);
        animator.SetBool("Fadeout", false);
    }
}
