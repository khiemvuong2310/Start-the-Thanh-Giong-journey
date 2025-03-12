using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    public void RestartGame()
    {
        MusicManager.Instance.PlayMusic("Game");
        SceneManager.LoadScene("Scene1"); 
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
