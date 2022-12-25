using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDGame : MonoBehaviour
{
    private float timeRemaining = 99;
    public Text countdown;
    public Text lifeNumber;
    public Text finalScore;

    public GameObject endgameMenu;
    public GameObject hud;


    // Start is called before the first frame update
    void Start()
    {
        countdown.text = Math.Round(timeRemaining).ToString();
        lifeNumber.text = GameManager.instance.lifes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        countdown.text = Math.Round(timeRemaining).ToString();
        lifeNumber.text = GameManager.instance.lifes.ToString();

        if (GameManager.instance.lifes == 0)
        {
            Time.timeScale = 0;
            finalScore.text = "0";
            hud.SetActive(false);
            endgameMenu.SetActive(true);
        }
    }

    public void NewGame()
    {
        timeRemaining = 99;
        Time.timeScale = 1;
        Player.respawByNewGame = true;
        hud.SetActive(true);
        endgameMenu.SetActive(false);
    }
    
    public void Finish()
    {
        Time.timeScale = 0;
        finalScore.text = GameManager.instance.score.ToString();
        hud.SetActive(false);
        endgameMenu.SetActive(true);
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void Quit()
    {
        MainMenu.QuitGame();
    }
    
}