using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float score;
    public int lifes;
    public AudioSource _AudioSource;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoseLife()
    {
        instance.lifes -= 1;
    }

    public static void AddScore(float points)
    {
        instance.score += points;
    }

    public static void NewGame()
    {
        instance.lifes = 3;
        instance.score = 0;
    }
}