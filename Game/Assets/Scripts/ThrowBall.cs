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
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");

        //float step = 0.2f * Time.deltaTime;


        Vector3 diff = Camera.main.ScreenToWorldPoint(direction) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 225);
    }



}
