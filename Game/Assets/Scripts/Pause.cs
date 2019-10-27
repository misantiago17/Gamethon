using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;

    private bool isActive = false;

    public void activatePauseMenu()
    {
        if (!isActive)
        {
            PauseMenu.SetActive(true);
            StartCoroutine(showPauseMenu());
            isActive = true;
        }
    }

    public void deactivatePauseMenu()
    {
        if (isActive)
        {
            //Time.timeScale = 1.0f;
            PauseMenu.SetActive(false);
            isActive = false;
        }
    }

    IEnumerator showPauseMenu()
    {
        bool waitAnimation = true;

        while (waitAnimation)
        {
            yield return new WaitForSeconds(PauseMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1);
            Time.timeScale = 0.0f;
            waitAnimation = false;
        }
    }
}
