using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    int score;
    int level;
    int layerCleared;

    bool gameIsOver;

    float fallSpeed;


    void Awake()
    {
        instance = this;
    }


    
    void Start()
    {
        //GameOverWindow.SetActive(false);
        SetScore(score);
    }
    public void SetScore(int amount)
    {
        score += amount;
        CalculateLevel();
        UIHandler.instance.UpdateUI(score, level, layerCleared);
        //Update UI
    }
    
    public float ReadFallSpeed()
    {
        return fallSpeed;
    }

    public void LayersCleared(int amount)
    {
        if (amount == 1)
        {
            SetScore(400);
        }
        else if (amount == 2)
        {
            SetScore(800);
        }
        else if (amount == 3)
        {
            SetScore(1600);
        }
        if (amount == 4)
        {
            SetScore(3200);
        }
        layerCleared += amount;
        //UPDATE UI
        UIHandler.instance.UpdateUI(score, level, layerCleared);
    }

    void CalculateLevel()
    {
        if (score <= 10000)
        {
            level = 1;
            fallSpeed = 3f;
        }
        else if (score > 10000 & score <= 20000)
        {
            level = 2;
            fallSpeed = 2.75f;
        }
        else if (score > 20000 & score <= 30000)
        {
            level = 3;
            fallSpeed = 2.50f;
        }
        else if (score > 30000 & score <= 40000)
        {
            level = 3;
            fallSpeed = 2.25f;
        }
        else if (score > 40000 & score <= 50000)
        {
            level = 4;
            fallSpeed = 2.00f;
        }
        else if (score > 50000 & score <= 60000)
        {
            level = 5;
            fallSpeed = 1.75f;
        }
        else if (score > 60000 & score <= 70000)
        {
            level = 6;
            fallSpeed = 1.50f;
        }
        else if (score > 70000 & score <= 80000)
        {
            level = 7;
            fallSpeed = 1.25f;
        }
        else if (score > 80000 & score <= 90000)
        {
            level = 9;
            fallSpeed = 1.00f;
        }
        else if (score > 100000)
        {
            level = 10;
            fallSpeed = 0.75f;
        }
        //UPDATE UI
    }

    public bool ReadGameIsOver()
    {
        return gameIsOver;
    }

    public void SetGameIsOver()
    {
        gameIsOver = true;
        UIHandler.instance.ActivateSetGameOverWindow();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
