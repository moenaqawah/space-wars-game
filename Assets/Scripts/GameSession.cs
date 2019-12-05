using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;


    private void Awake()
    {
        setUpSingleton();
    }

    private void setUpSingleton()
    {
        int length = FindObjectsOfType<GameSession>().Length;
        if (length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int getScore()
    {
        return score;
    }

    public void addToScore(int value)
    {
        score += value;
    }

    public void restGameSession()
    {
        Destroy(gameObject);
    }
}
