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

    public void ThrowBallInDirection(Vector3 direction, int value, GameObject ball)
    {
        ball.GetComponent<BallData>().updateNum(value);
        ball.GetComponent<Rigidbody2D>().velocity = Force * direction;
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");

        //Rotate Pointer towards mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gameObject.transform.GetChild(0).transform.position);

    }

    }




