using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public float Force = 10f;

    private void Start()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void ThrowBallInDirection(Vector3 direction)
    {
        // Forca ta muito lunar
        this.GetComponent<Rigidbody2D>().velocity = Force * direction;
    }



}
