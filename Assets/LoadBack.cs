using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBack : MonoBehaviour
{
    public void StartScrene()
    {
        StartCoroutine(LoadStart());
    }

    IEnumerator LoadStart()
    {
        yield return new WaitForSeconds(0.35f);
        SceneManager.LoadScene(0);
    }
}
