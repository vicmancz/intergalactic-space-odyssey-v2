using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayGame()
    {
        SceneManager.LoadScene(1);
        GameManager.NewGame();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
