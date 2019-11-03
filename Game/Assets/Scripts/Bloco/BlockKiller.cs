using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockKiller : MonoBehaviour
{
    public GameObject LosePanel;
    public GameObject Grid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            Grid.GetComponent<GridManager>().RemoveBlockFromLine(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
