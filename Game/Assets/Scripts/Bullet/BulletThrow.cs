using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrow : MonoBehaviour
{
    public float Force = 10f;
    public AudioSource audioTiro;

    private void Start() {
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void ThrowBallInDirection(Vector3 direction) {

        this.GetComponent<Rigidbody2D>().velocity = Force * direction;

        if (gameObject.transform.GetChild(0).GetComponent<Animator>())
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
        if (gameObject.GetComponentInChildren<ParticleSystem>())
            gameObject.GetComponentInChildren<ParticleSystem>().Play();

        if(audioTiro)
            audioTiro.PlayOneShot(audioTiro.clip, audioTiro.volume);

        //Rotate Pointer towards mouse ----- !
       // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // gameObject.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gameObject.transform.GetChild(0).transform.position);

    }
}
