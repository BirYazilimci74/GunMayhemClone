using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public bool canPlay = true;
    private void Awake()
    {
        if (Instance is not null && Instance == this)
        {
            return;
        }
        Instance = this;
    }

    public void EndingGame()
    {
        canPlay = false;
        UIManager.Instance.gameOverScreen.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

}
