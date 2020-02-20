﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        Lose.Instance.deactivateLoseMenu();
        SceneManager.LoadScene(2);
    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
