using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    float waitTime = 4f;

    [SerializeField]
    float sceneLoadTime =1f;

    
    public void LoadEndScene()
    {
        
        StartCoroutine(WaitBeforePlaying());
        
    }

    IEnumerator WaitBeforePlaying()
    {
        yield return new WaitForSeconds(waitTime);
        FindObjectOfType<GameManager>().StopMusic();
        SceneManager.LoadScene("GameOver");
    }
    

    public void LoadGame()
    {
        
        StartCoroutine(GameLoad(sceneLoadTime));
    }

    IEnumerator GameLoad(float loadTime)
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene("TheGame");

    }

    public void LoadHelp()
    {
        StartCoroutine(HelpLoad(sceneLoadTime));
    }

    IEnumerator HelpLoad(float loadTime)
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene("Rules");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
    
}
