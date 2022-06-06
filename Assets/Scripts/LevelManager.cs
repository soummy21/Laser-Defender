using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    float sceneLoadTime = 1f;
    public void LoadStartScreen()
    {

        StartCoroutine(StartScreen(sceneLoadTime));


    }

    IEnumerator StartScreen(float loadTime)
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(0);
        FindObjectOfType<GameManager>().RestartScore();


    }

    public void LoadTheGame()
    {

        StartCoroutine(GameLoad(sceneLoadTime));
    }

    IEnumerator GameLoad(float loadTime)
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene("TheGame");

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            FindObjectOfType<GameManager>().RestartScore();

        }

    }
}