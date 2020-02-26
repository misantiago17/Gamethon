using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public bool GameOver = false;

    [HideInInspector] public int currentScore = 0;
    public Text scoreText;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void UpdateHUD(int value)
    {
        currentScore += value;

        if (scoreText)
            scoreText.text = currentScore.ToString();
    }

}
