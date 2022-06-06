using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;
using System;
using System.Security.Cryptography;

public class GameManager : MonoBehaviour
{
    

    int totalscore = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        CallSingleton();
    }

    private void CallSingleton()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    public void CalculateScore(int actualScore)
    {
        totalscore += actualScore;
        
    }    

    public int GetTotalScore()
    {
        return totalscore;
    }

    public void RestartScore()
    {
        Destroy(gameObject);
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}
