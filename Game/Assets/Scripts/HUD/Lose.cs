﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose: MonoBehaviour
{
    #region Singleton

    private static Lose _instance;
    public static Lose Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    #endregion

    public GameObject LoseMenu;

    [HideInInspector] public bool isOnLoseMenu = false;

    public void activateLoseMenu() {
        if (!isOnLoseMenu) {
            LoseMenu.SetActive(true);
            StartCoroutine(showLoseMenu());
            Time.timeScale = 0.0f;
            isOnLoseMenu = true;
        }
    }

    public void deactivateLoseMenu() {
        if (isOnLoseMenu) {
            Time.timeScale = 1.0f;
            LoseMenu.SetActive(false);
            isOnLoseMenu = false;
        }
    }

    IEnumerator showLoseMenu() {
        bool waitAnimation = true;

        while (waitAnimation) {
            yield return new WaitForSeconds(LoseMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1);
            //Time.timeScale = 0.0f;
            waitAnimation = false;
        }
    }
}
