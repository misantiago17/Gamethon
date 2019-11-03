using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.transform.CompareTag("Tirinho")) {
            Destroy(collision.gameObject);
        }
    }
}
