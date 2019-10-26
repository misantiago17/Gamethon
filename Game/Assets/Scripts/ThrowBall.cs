using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject tirinho;

    public float Force = 10f;

    private void Start()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void ThrowBallInDirection(Vector3 direction, int value)
    {
        // Forca ta muito lunar
        GameObject tirin = GameObject.Instantiate(tirinho, this.transform.position, this.transform.rotation);
        tirin.GetComponent<BallData>().updateNum(value);
        tirin.GetComponent<Rigidbody2D>().velocity = Force * direction;
    }



}
