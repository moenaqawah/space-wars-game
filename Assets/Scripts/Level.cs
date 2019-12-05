using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float gameOverDelaySeconds=2f;
    


    IEnumerator gameOverWithDelay()
    {
        yield return new WaitForSeconds(gameOverDelaySeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void loadGameOverScene()
    {
        StartCoroutine(gameOverWithDelay());
    }

    public void loadStartMenuScene()
    {
        FindObjectOfType<GameSession>().restGameSession();
        SceneManager.LoadScene("Start Menu");
    }


    public void loadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
