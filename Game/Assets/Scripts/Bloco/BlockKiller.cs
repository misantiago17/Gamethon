﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            GridManager.Instance.RemoveBlockFromLine(collision.gameObject);
            Destroy(collision.gameObject);

            this.GetComponent<Lose>().activateLoseMenu();

        }
    }
}
