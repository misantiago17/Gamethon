using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    #region Singleton

    private static Pause _instance;
    public static Pause Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    #endregion

    public GameObject PauseMenu;

    [HideInInspector] public bool isOnPause = false;

    public void activatePauseMenu()
    {
        if (!isOnPause)
        {
            PauseMenu.SetActive(true);
            StartCoroutine(showPauseMenu());
            Time.timeScale = 0.0f;
            isOnPause = true;
        }
    }

    public void deactivatePauseMenu()
    {
        if (isOnPause)
        {
            Time.timeScale = 1.0f;
            PauseMenu.SetActive(false);
            isOnPause = false;
        }
    }

    IEnumerator showPauseMenu()
    {
        bool waitAnimation = true;

        while (waitAnimation)
        {
            yield return new WaitForSeconds(PauseMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1);
            //Time.timeScale = 0.0f;
            waitAnimation = false;
        }
    }
}
