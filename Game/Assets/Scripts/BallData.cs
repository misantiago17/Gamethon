using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData : MonoBehaviour
{
    [HideInInspector] public int CurrentNum = 1;

    public void updateNum(int num)
    {
        CurrentNum = num;
    }

    public int getNum()
    {
        return CurrentNum;
    }
}
