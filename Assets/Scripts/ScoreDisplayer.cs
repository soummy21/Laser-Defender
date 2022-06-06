using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update

    GameManager score;
    void Start()
    {
        score = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.GetTotalScore().ToString();
    }
}
