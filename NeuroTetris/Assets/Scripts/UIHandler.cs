using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    public static UIHandler instance;

    public Text scoreText;
    public Text leveltext;
    public Text layersText;
    public GameObject GameOverWindow;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameOverWindow.SetActive(false);
    }

    public void UpdateUI(int score, int level, int layers)
    {
        scoreText.text = "Score: " + score.ToString("D9");
        leveltext.text = "Level: " + level.ToString("D2");
        layersText.text = "Layers: " + layers.ToString("D9");
    }

    public void ActivateSetGameOverWindow()
    {
        GameOverWindow.SetActive(true);
    }
}
