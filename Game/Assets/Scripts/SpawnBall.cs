using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    // velocidade setda como 0
    // trigga o hold de forca e mira

    public Vector3 initialPosition = new Vector3(0, -3, 0);

    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void respawnBall()
    {
        this.transform.position = initialPosition;
        rb.velocity = Vector2.zero;
    }

    public void respawnBall(float blockX)
    {
        this.transform.position = new Vector3(blockX,-3,0);
        rb.velocity = Vector2.zero;
    }
}
