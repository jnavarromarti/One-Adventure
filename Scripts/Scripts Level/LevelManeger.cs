using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManeger : MonoBehaviour
{
    HeroController heroController;
    public Transform hero;
    public bool levelComplete;
    public float timeToWait;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        heroController = hero.GetComponent<HeroController>();
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
    IEnumerator LoadNextLevel()
    {
        animator.SetBool("Fadeout", true);
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("Wood");
        
    }
}
