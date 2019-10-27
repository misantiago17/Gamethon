using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData : MonoBehaviour
{
    [HideInInspector] public int CurrentNum = 1;
    public Sprite[] sprites;

    public void updateNum(int num)
    {
        CurrentNum = num;

        
    }
    public void Update()
    {
        if (CurrentNum == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }

        if (CurrentNum == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }

        if (CurrentNum == 4)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        if (CurrentNum == 8)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        if (CurrentNum == 16)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
        }
    }

    public int getNum()
    {
        return CurrentNum;
    }
}
