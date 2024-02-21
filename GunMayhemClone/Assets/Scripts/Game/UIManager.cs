using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static UIManager Instance { get; private set;}

    private void Awake()
    {
        if (Instance is not null && Instance == this)
        {
            return;
        }
        Instance = this;
    }

}
