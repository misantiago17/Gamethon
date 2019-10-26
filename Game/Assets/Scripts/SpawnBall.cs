using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    // velocidade setda como 0
    // trigga o hold de forca e mira

    public Vector3 initialPosition = new Vector3(0, -3, 0);


    private void Start()
    {
    }

    public void respawnBall()
    {
        this.transform.position = initialPosition;
    }

    public void respawnBall(float blockX)
    {
        this.transform.position = new Vector3(blockX,-3,0);
    }
}
